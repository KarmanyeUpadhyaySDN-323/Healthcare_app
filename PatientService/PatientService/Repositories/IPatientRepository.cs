using PatientService.Model;

namespace PatientService.Repositories
{
    public interface IPatientRepository
    {
        Task AddPatientAsync(Patient patient);
        Task<Patient?> GetPatientByEmailAsync(string email);
        Task<List<Patient?>> GetPatient();
        Task SendEmailAsync(string to, string subject, string body);
    }
}
