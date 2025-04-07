using Appointment.DTO;
using AppointmentService.Services;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Controllers
{
    public class AppointmentController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreateAppointment([FromBody] AppointmentDto appointment)
        {
            var publisher = new RabbitMQPublisher();
            var message = $"Appointment: Patient={appointment.PatientName}, Staff={appointment.StaffName}, Time={appointment.Time}";
            publisher.SendMessage(message);
            return Ok(new { Status = "Appointment Created and Sent to Staff" });
        }
    }
}
