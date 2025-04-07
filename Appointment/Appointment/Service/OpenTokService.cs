using Appointment.DTO;
using Microsoft.Extensions.Options;
using OpenTokSDK;
using System.Data;
using System.Net;

namespace Appointment.Service
{
    public class OpenTokService : IOpenTokService
    {
        private readonly OpenTok _openTok;
        private readonly string _apiKey;

        public OpenTokService(IOptions<OpenTokSettings> settings)
        {
            _apiKey = settings.Value.ApiKey;
            _openTok = new OpenTok(Convert.ToInt32(settings.Value.ApiKey), settings.Value.ApiSecret);
        }

        public string GetApiKey() => _apiKey;

        public string CreateSession()
        {
            var session = _openTok.CreateSession();
            return session.Id;
        }

        public string GenerateToken(string sessionId)
        {
            return _openTok.GenerateToken(sessionId);
        }
    }
}
