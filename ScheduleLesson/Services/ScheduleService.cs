﻿using Microsoft.EntityFrameworkCore;
using ScheduleLesson.Models;
using System.Net;

namespace ScheduleLesson.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<ScheduleService> _logger;
        public ScheduleService(ApiDbContext context, ILogger<ScheduleService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<Schedule>> GetAllSchedule(Guid userGuid)
        {
            Task<List<Schedule>> result = _context.Schedules
                .Where(s => s.UserId == userGuid)
                .ToListAsync();
            return await result;
        }
        public async Task<Schedule> GetScheduleById(int id, Guid userGuid)
        {
            Schedule? result = await _context.Schedules
                .Where(s => s.UserId == userGuid && s.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Schedule> AddSchedule(Schedule schedule, Guid userGuid)
        {
            schedule.UserId = userGuid;
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return await GetScheduleById(schedule.Id, userGuid); ;
        }
        public async Task<Schedule> UpdateSchedule(Schedule updateSchedule, Guid userGuid)
        {
            _logger.LogInformation("Зміна користувача з ID: {updateSchedule}", updateSchedule);
            Schedule? result = await _context.Schedules
                .Where(s => s.UserId == userGuid && s.Id == updateSchedule.Id)
                .FirstOrDefaultAsync();
            if (result == null)
            {
                throw new StatusCodeException($"Користувача з ID: {updateSchedule.Id} не знайдено", (int)HttpStatusCode.NotFound);
            }
            result.DateTime = updateSchedule.DateTime;
            result.Order = updateSchedule.Order;
            result.Content = updateSchedule.Content;
            result.ClassName = updateSchedule.ClassName;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Кінець зміни користувача з ID: {updateSchedule}", userGuid);
            return updateSchedule;
        }
        public async Task<Schedule> DeleteSchedule(int id, Guid userGuid)
        {
            Schedule? result = await _context.Schedules
                            .Where(s => s.UserId == userGuid && s.Id == id)
                            .FirstOrDefaultAsync();
            _context.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }
        public async Task<List<string>> GetDateTimeSchedule(DateTime dateTime, Guid userGuid)
        {
            return await _context.Schedules.Where(x => x.UserId == userGuid && x.DateTime.Date == dateTime.Date).Select(x => $"{x.Order}. {x.ClassName} {x.Content}").ToListAsync();
        }
    }
}
