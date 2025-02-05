using ScheduleLesson.Models;

namespace ScheduleLesson.Services
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetAllSchedule(string? userId);
        Task<Schedule> GetScheduleById(int id, string? userId);
        Task<Schedule> AddSchedule(Schedule schedule, string userId);
        Task<Schedule> UpdateSchedule(Schedule schedule, string userId);
        Task<Schedule> DeleteSchedule(int id, string userId);
        Task<List<string>> GetDateTimeSchedule(DateTime dateTime, string userId);
    }
}
