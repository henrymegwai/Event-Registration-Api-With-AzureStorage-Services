using Microsoft.AspNetCore.Mvc;
using Registration.Attendance.Application;
using Registration.Attendance.Application.Models;
using Registration.Domain;
using Registration.Infrastructure.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Registration.Attendance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly ITableStorageService _storageService;

        public AttendanceController(ITableStorageService tableStorageService)
        {
            _storageService = tableStorageService;
        }

        // GET: api/<AttendanceRegistrationController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<AttendeeEntity> attendeeList = await _storageService.GetAttendees();
            return Ok(attendeeList);
        }

        // GET api/<AttendanceController>/5
        [HttpGet("{id}/{industry}")]
        public async Task<IActionResult> Get(string id, string industry)
        {
            var attendee = await _storageService.GetAttendees();
            if (attendee == null)
                return NotFound();
            return Ok(attendee);
        }

        // POST api/<AttendanceController>
        [HttpPost("signup")]
        public async Task<IActionResult> Post([FromBody] AttendanceModel model)
        {
            if (model == null)
            {
                return BadRequest("Validation error, Attendance is required");
            }
            await _storageService.UpsertAttendee(new AttendeeEntity
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                Industry = model.Industry
            });
            return Accepted();
        }

        // DELETE api/<AttendanceController>/5
        [HttpDelete("{id}/{industry}")]
        public async Task<IActionResult> Delete(string id, string industry)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(industry))
                return BadRequest("id or industry is missing");
            await _storageService.DeleteAttendee(industry, id);
            return Ok();
        }
    }
}
