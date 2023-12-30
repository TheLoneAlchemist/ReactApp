using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Constants;
using ReactApp1.Server.Interfaces;
using ReactApp1.Server.Models.DTOs;

namespace ReactApp1.Server.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Register( RegisterDTO registerDTO)
        {

            if (registerDTO == null)
            {
                return BadRequest();
            }
            var (status, msg) = await _authService.RegisterAsync( registerDTO, UserRole.Administrator);
            return Ok(new { status = status, msg = msg });
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest();
            }
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var (accessToken, refreshToken) = await _authService.LoginAsync(loginDTO, cancellationToken.Token);
            if (String.IsNullOrEmpty(accessToken) || String.IsNullOrEmpty(refreshToken))
            {
                return (Ok(new { status = false, msg="Invalid Login! Please Try again..." }));
            }
            return Ok(new { status=true,accessToken= accessToken, refreshToken= refreshToken });;

        }

        [HttpPost]
        public async Task<IActionResult> Refresh(RefreshTokenDTO refreshTokenDTO)
        {
            if (refreshTokenDTO.RefreshToken is null || refreshTokenDTO.AccessToken is null)
            {
                return Unauthorized();
            }

            var (accessToken,refreshToken, statusCode) =  await _authService.Refresh(refreshTokenDTO.AccessToken, refreshTokenDTO.RefreshToken);
            if (statusCode == StatusCodes.Status401Unauthorized)
            {
                return Unauthorized();

            }

            return Ok(new { status = true, accessToken = accessToken, refreshToken = refreshToken }); ;

        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        public async Task<IActionResult> Revoke()
        {
            var status = await _authService.Revoke();
            return status==200 ? Ok() : Unauthorized();
        }
    }
}
