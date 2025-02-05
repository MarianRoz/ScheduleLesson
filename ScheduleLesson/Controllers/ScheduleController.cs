using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleLesson.Models;
using ScheduleLesson.Services;
using System.Security.Claims;

namespace ScheduleLesson.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<ActionResult<Schedule>> GetAllSchedule()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Отримуємо ідентифікатор користувача
            List<Schedule> result = await _scheduleService.GetAllSchedule(userId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Schedule? scheduleById = await _scheduleService.GetScheduleById(id, userId);
            if (scheduleById is null)
                return NotFound("Schedule not found");
            return Ok(scheduleById);
        }

        [HttpPost]
        public async Task<ActionResult<Schedule>> AddSchedule(Schedule schedule)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Schedule result = await _scheduleService.AddSchedule(schedule, userId);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<Schedule>> UpdateSchedule(Schedule Id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Schedule? dbScheduleUpdated = await _scheduleService.UpdateSchedule(Id, userId);
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Schedule>>> DeleteSchedule(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Schedule? scheduleDeleted = await _scheduleService.DeleteSchedule(id, userId);
            if (scheduleDeleted is null)
                return NotFound("Schedule not found");
            return Ok(scheduleDeleted);
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> GetDateTimeSchedule(DateTime dateTime)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<string> result = await _scheduleService.GetDateTimeSchedule(dateTime, userId);
            return Ok(result);

        }
    }
}
