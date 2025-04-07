using MimeKit;

namespace StaffService.Model
{
    public class Message
    {
        public List<MailboxAddress> To { get; }
        public string Subject { get; }
        public string Content { get; }

        public Message(List<MailboxAddress> to, string subject, string content)
        {
            To = to;
            Subject = subject;
            Content = content;
        }
    }
}
