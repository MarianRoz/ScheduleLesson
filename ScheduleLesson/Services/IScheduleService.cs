using ScheduleLesson.Models;

namespace ScheduleLesson.Services
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetAllSchedule();
        Task<Schedule> GetSchedule(int id);
        Task<Schedule> AddSchedule(Schedule schedule);
        Task<Schedule> UpdateSchedule(Schedule schedule);
        Task<Schedule> DeleteSchedule(int id);
        Task<List<string>> GetDateTimeSchedule(DateTime dateTime);

    }
}
