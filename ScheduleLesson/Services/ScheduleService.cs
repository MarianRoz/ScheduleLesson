using Microsoft.EntityFrameworkCore;
using ScheduleLesson.Models;

namespace ScheduleLesson.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApiDbContext _context;
        public ScheduleService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Schedule>> GetAllSchedule()
        {
            Task<List<Schedule>> result = _context.Schedules.ToListAsync();
            return await result;
        }
        public async Task<Schedule> GetSchedule(int id)
        {
            return await _context.Schedules.FindAsync(id);
        }
        public async Task<Schedule> AddSchedule(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return await GetSchedule(schedule.Id);
        }

        public async Task<Schedule> UpdateSchedule(Schedule updateSchedule)
        {
            try
            {
                _context.Entry(updateSchedule).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return updateSchedule;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new CustomException("Id not found", 404);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
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
