using Microsoft.AspNetCore.Mvc;
using ScheduleLesson.Models;
using ScheduleLesson.Services;

namespace ScheduleLesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        [HttpPost]
        public async Task<User> CreateUser(User user)
        {
            User result = await _userService.CreateUser(user);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            User? result = await _userService.GetUserById(id);
            if (result is null)
                return NotFound("Schedule not found");
            return Ok(result);
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
