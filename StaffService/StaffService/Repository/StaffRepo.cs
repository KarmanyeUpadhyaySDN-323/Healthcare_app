using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using StaffService.Dbfolder;
using StaffService.Model;

namespace StaffService.Repository
{
    public class staffRepository : IStaffRepo
    {
        private readonly StaffDbContext _context;
        private readonly EmailSettings _emailSettings;
        public staffRepository(StaffDbContext context, IOptions<EmailSettings> emailSettings)
        {
            _context = context;
            _emailSettings = emailSettings.Value;
        }

        public async Task AddstaffAsync(Staff staff)
        {
            await _context.Staffs.AddAsync(staff);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Staff?>> Getstaff()
        {
            return await _context.Staffs.ToListAsync();

        }

        public async Task<Staff?> GetstaffByEmailAsync(string email)
        {
            return await _context.Staffs.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Your App Name", _emailSettings.SenderEmail));
            email.To.Add(new MailboxAddress("", to));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls); // Fix SSL issue
            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
