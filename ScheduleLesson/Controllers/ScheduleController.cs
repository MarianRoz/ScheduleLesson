using Microsoft.AspNetCore.Mvc;
using ScheduleLesson.Models;
using ScheduleLesson.Services;

namespace ScheduleLesson.Controllers
{
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
            List<Schedule> result = await _scheduleService.GetAllSchedule();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            Schedule? scheduleById = await _scheduleService.GetSchedule(id);
            if (scheduleById is null)
                return NotFound("Schedule not found");
            return Ok(scheduleById);
        }

        [HttpPost]
        public async Task<ActionResult<Schedule>> AddSchedule(Schedule schedule)
        {
            Schedule result = await _scheduleService.AddSchedule(schedule);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<Schedule>> UpdateSchedule(Schedule Id)
        {
            Schedule? dbScheduleUpdated = await _scheduleService.UpdateSchedule(Id);
            if (dbScheduleUpdated is null)
                return NotFound("Schedule not found");
            return Ok("Updated Successfully");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Schedule>>> DeleteSchedule(int id)
        {
            Schedule? scheduleDeleted = await _scheduleService.DeleteSchedule(id);
            if (scheduleDeleted is null)
                return NotFound("Schedule not found");
            return Ok(scheduleDeleted);
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> GetDateTimeSchedule(DateTime dateTime)
        {
            List<string> result = await _scheduleService.GetDateTimeSchedule(dateTime);
            return Ok(result);

        }
    }
}
