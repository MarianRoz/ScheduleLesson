using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleLesson.Models;

namespace ScheduleLesson.Services
{
    public class UserService : IUserService
    {
        private readonly ApiDbContext _context;
        public UserService(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUserById(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            return user;
        }
        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return await GetUserById(user.Id);
        }
    }
}
