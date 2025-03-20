using ScheduleLesson.Models;

namespace ScheduleLesson.Services
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetAllSchedule(Guid userGuid);
        Task<Schedule> GetScheduleById(int id, Guid userGuid);
        Task<Schedule> AddSchedule(Schedule schedule, Guid userGuid);
        Task<Schedule> UpdateSchedule(Schedule schedule, Guid userGuid);
        Task<Schedule> DeleteSchedule(int id, Guid userGuid);
        Task<List<string>> GetDateTimeSchedule(DateTime dateTime, Guid userGuid);
    }
}
