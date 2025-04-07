using Microsoft.AspNetCore.Mvc;
using StaffService.Model;
using StaffService.Repository;

namespace StaffService.Controllers
{
    [Route("staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffRepo _staffRepository;

        public StaffController(IStaffRepo staffRepository)
        {
            _staffRepository = staffRepository;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Registerstaff([FromBody] RegisterstaffDto dto)
        {
            if (await _staffRepository.GetstaffByEmailAsync(dto.Email) != null)
            {
                return BadRequest("Email is already registered.");
            }
            string generatedPassword = GenerateRandomPassword();
            var staff = new Staff
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.Phone,
                PasswordHash = generatedPassword,
                RegisteredOn = DateTime.UtcNow
            };





            await _staffRepository.AddstaffAsync(staff);
            string emailBody = $"<h2>Welcome to Our Platform</h2><p>Your account has been created successfully.</p><p><strong>Temporary Password:</strong> {generatedPassword}</p><p>Please change your password after logging in.</p>";
            await _staffRepository.SendEmailAsync(dto.Email, "Registration Successful", emailBody);


            return Ok(new { Message = "Registration successful" });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Getstaff()
        {
            var data = _staffRepository.Getstaff();
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
    public class RegisterstaffDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

}
