using Microsoft.EntityFrameworkCore;
using ScheduleLesson.Models;

namespace ScheduleLesson.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<ScheduleService> _logger;
        public const int StatusCodeInternalServerError = 404;


        public ScheduleService(ApiDbContext context, ILogger<ScheduleService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<Schedule>> GetAllSchedule()
        {
            Task<List<Schedule>> result = _context.Schedules.ToListAsync();
            return await result;
        }
        public async Task<Schedule> GetSchedule(int id)
        {
            Schedule? result = await _context.Schedules.FindAsync(id);
            return result;
        }
        public async Task<Schedule> AddSchedule(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return await GetSchedule(schedule.Id);
        }
        public async Task<Schedule> UpdateSchedule(Schedule updateSchedule)
        {
            _logger.LogInformation("Зміна користувача з ID: {updateSchedule}", updateSchedule);
            Schedule? result = await _context.Schedules.FindAsync(updateSchedule.Id);
            if (result == null)
            {
                throw new StatusCodeException($"Користувача з ID: {updateSchedule.Id} не знайдено", StatusCodeInternalServerError);
            }
            result.DateTime = updateSchedule.DateTime;
            result.Order = updateSchedule.Order;
            result.Content = updateSchedule.Content;
            result.ClassName = updateSchedule.ClassName;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Кінець зміни користувача з ID: {updateSchedule}", updateSchedule);

            return updateSchedule;
        }
        public async Task<Schedule> DeleteSchedule(int id)
        {
            Schedule? res = await _context.Schedules.FindAsync(id);
            _context.Remove(res);
            await _context.SaveChangesAsync();
            return res;
        }
        public async Task<List<string>> GetDateTimeSchedule(DateTime dateTime)
        {
            return await _context.Schedules.Where(x => x.DateTime.Date == dateTime.Date).Select(x => $"{x.Order}. {x.ClassName} {x.Content}").ToListAsync();
        }

    }
}
