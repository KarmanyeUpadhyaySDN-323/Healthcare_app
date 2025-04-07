using Appointment.DTO;

namespace Appointment.Service
{
    public interface IOpenTokService
    {
        string GetApiKey();
        string CreateSession();
        string GenerateToken(string sessionId);
    }
}
