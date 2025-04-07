using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace AppointmentService.Services
{
    public class RabbitMQPublisher
    {
        public void SendMessage<T>(T message)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "staff_registered", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: "staff_registered", basicProperties: null, body: body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RabbitMQ ERROR] {ex.Message}");

            }
        }
    }
}