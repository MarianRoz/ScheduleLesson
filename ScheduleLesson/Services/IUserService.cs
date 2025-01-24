using Microsoft.AspNetCore.Mvc;
using ScheduleLesson.Models;

namespace ScheduleLesson.Services
{
    public interface IUserService
    {
        Task<ActionResult<IEnumerable<User>>> GetAllUsers();
        Task<User> CreateUser(User user);
        Task<User> GetUserById(int id);
    }
}
