using Microsoft.EntityFrameworkCore;
using ScheduleLesson.Models;

namespace ScheduleLesson
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        public DbSet<Schedule> Schedules { get; set; }
    }
}
