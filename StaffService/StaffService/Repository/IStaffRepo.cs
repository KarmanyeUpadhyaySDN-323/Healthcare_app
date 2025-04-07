using StaffService.Model;

namespace StaffService.Repository
{
    public interface IStaffRepo
    {
        Task AddstaffAsync(Staff staff);
        Task<Staff?> GetstaffByEmailAsync(string email);
        Task<List<Staff?>> Getstaff();
        Task SendEmailAsync(string to, string subject, string body);
    }
}
