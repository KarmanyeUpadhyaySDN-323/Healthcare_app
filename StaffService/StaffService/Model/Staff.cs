namespace StaffService.Model
{
    public class Staff
    {
        public Guid Id { get; set; } // Primary Key
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;
    }
}
