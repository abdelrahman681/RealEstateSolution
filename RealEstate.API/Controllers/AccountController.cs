using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Error;
using RealEstate.Domain.DTO;
using RealEstate.Domain.InterFace.Services;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _userService.LoginAsync(login);
            return user is not null ? Ok(user) : Unauthorized(new APIResponse(401, "In Correct Password Or Email"));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            var user = await _userService.RegisterAsync(register);
            return Ok(user);
        }
    }
}
