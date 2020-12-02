using Microsoft.AspNetCore.Mvc;
using RecruitTask.Models.Services;
using RecruitTask.Models;
using Microsoft.AspNetCore.Authorization;

namespace RecruitTask.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(AuthenticateRequest model)
        {
            var response = userService.Login(model);

            if (response == null)
                return BadRequest(new { message = "Login or password is incorrect" });

            return Ok(response);
        }
    }
}