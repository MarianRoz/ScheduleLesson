using Microsoft.AspNetCore.Mvc;
using ScheduleLesson.Models;
using ScheduleLesson.Services;

namespace ScheduleLesson.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetAllUser()
        {
            ActionResult<IEnumerable<User>> result = await _userService.GetAllUsers();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            User? userById = await _userService.GetUserById(id);
            if (userById is null)
                return NotFound("Schedule not found");
            return Ok(userById);
        }
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            User result = await _userService.CreateUser(user);
            return Ok(result);
        }
    }
}
