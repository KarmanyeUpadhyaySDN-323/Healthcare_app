using Microsoft.AspNetCore.Mvc;
using PatientService.Model;
using PatientService.Repositories;
using System.Text;
using System.Security.Cryptography;

namespace PatientService.Controllers
{
    [Route("patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientDto dto)
        {
            if (await _patientRepository.GetPatientByEmailAsync(dto.Email) != null)
            {
                return BadRequest("Email is already registered.");
            }
            string generatedPassword = GenerateRandomPassword();
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.Phone,
                PasswordHash = generatedPassword,
                RegisteredOn = DateTime.UtcNow
            };





            await _patientRepository.AddPatientAsync(patient);
            string emailBody = $"<h2>Welcome to Our Platform</h2><p>Your account has been created successfully.</p><p><strong>Temporary Password:</strong> {generatedPassword}</p><p>Please change your password after logging in.</p>";
            await _patientRepository.SendEmailAsync(dto.Email, "Registration Successful", emailBody);


            return Ok(new { Message = "Registration successful" });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPatient()
        {
            var data = _patientRepository.GetPatient();
            return Ok(new { Message = "Registration successful" });

        }
        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#$%";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
    public class RegisterPatientDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
