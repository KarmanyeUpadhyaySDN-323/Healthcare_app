using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MimeKit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StaffService.Model;
using StaffService.Repository;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace StaffService
{
    public class MessageConsumer
    {
        private readonly IStaffRepo _sendMailService;

        public MessageConsumer(IStaffRepo sendMailService)
        {
            _sendMailService = sendMailService;
        }

        public void Start()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest"
                    // Port = 5672 // default, can omit
                };

                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();

                channel.QueueDeclare(
                    queue: "staff_registered",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var json = Encoding.UTF8.GetString(body);
                        var staff = JsonSerializer.Deserialize<StaffRegistrationDto>(json);

                        if (staff != null && !string.IsNullOrEmpty(staff.Email))
                        {
                            var toAddresses = new List<MailboxAddress>
                            {
                                new MailboxAddress(staff.FirstName, staff.Email)
                            };

                            await _sendMailService.SendEmailAsync("karma@yopmail.com","test","Call started"); // Make async if your service supports it
                        }
                        else
                        {
                            Console.WriteLine("[RabbitMQ] Received invalid staff data.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[RabbitMQ Consumer ERROR] Failed to process message: {ex.Message}");
                    }
                };

                channel.BasicConsume(
                    queue: "staff_registered",
                    autoAck: true,
                    consumer: consumer
                );

                Console.WriteLine("[RabbitMQ] Consumer started and listening on 'staff_registered' queue.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RabbitMQ ERROR] Failed to connect or consume: {ex.Message}");
            }
        }
    }
    public class StaffRegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
