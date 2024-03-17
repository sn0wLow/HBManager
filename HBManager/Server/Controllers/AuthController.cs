using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HBManager.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(UserRegisterDTO request)
        {
            var now = DateTime.Now;
            var response = await authService.Register(new User { Name = request.Name, CreationDate = now, LastOnlineDate = now }, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<User>>> Login(UserLoginDTO request)
        {
            var response = await this.authService.Login(request.Name, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }


            return Ok(response);
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] string newPassword)
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await this.authService.ChangePassword(int.Parse(userID!), newPassword);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }



    }
}
