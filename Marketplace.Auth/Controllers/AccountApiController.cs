using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountApiController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountApiController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("~/Login")]
        public async Task<bool> Login(LoginDto model)
        {
            return await _accountService.Login(model);
        }

        [HttpPost("~/Register")]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            var result = await _accountService.Register(model);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost("~/Logout")]
        [Authorize]
        public async Task Logout()
        {
            await _accountService.Logout();
        }

        [HttpPut("~/EditUser")]
        [Authorize]
        public async Task<ApplicationUser> EditUser(EditUserDto model)
        {
            return await _accountService.Edit(model, User?.Identity?.Name);
        }

        [HttpDelete("~/Delete")]
        [Authorize]
        public async Task<bool> Delete()
        {
            return await _accountService.Delete(User?.Identity?.Name);
        }
    }
}
