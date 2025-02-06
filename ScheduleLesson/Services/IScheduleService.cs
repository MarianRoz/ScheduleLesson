using ScheduleLesson.Models;

namespace ScheduleLesson.Services
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetAllSchedule(Guid userId);
        Task<Schedule> GetScheduleById(int id, Guid userId);
        Task<Schedule> AddSchedule(Schedule schedule, Guid userId);
        Task<Schedule> UpdateSchedule(Schedule schedule, Guid userId);
        Task<Schedule> DeleteSchedule(int id, Guid userId);
        Task<List<string>> GetDateTimeSchedule(DateTime dateTime, Guid userId);
    }
}
