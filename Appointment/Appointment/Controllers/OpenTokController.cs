using Appointment.Service;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenTokController : ControllerBase
    {
        private readonly IOpenTokService _openTokService;

        public OpenTokController(IOpenTokService openTokService)
        {
            _openTokService = openTokService;
        }

        [HttpGet("session")]
        public IActionResult CreateSession()
        {
            var sessionId = _openTokService.CreateSession();
            var token = _openTokService.GenerateToken(sessionId);
            return Ok(new
            {
                apiKey = _openTokService.GetApiKey(),
                sessionId,
                token
            });
        }

        [HttpGet("token/{sessionId}")]
        public IActionResult GetToken(string sessionId)
        {
            var token = _openTokService.GenerateToken(sessionId);
            return Ok(new
            {
                apiKey = _openTokService.GetApiKey(),
                sessionId,
                token
            });
        }
    }
}
