using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Commons.Services;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IProfileService _profileService; 
        public AuthController(AuthService authService, IProfileService profileService)
        {
            _authService = authService;
            _profileService = profileService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var (accessToken, refreshToken, user) = await _authService.LoginAsync(loginModel.EmailorPhone, loginModel.Password);

            if (accessToken == null || refreshToken == null || user == null)
            {
                return BadRequest(new
                {
                    Succeeded = false,
                    Message = "Invalid username or password."
                });
            }

            return Ok(new
            {
                Succeeded = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                User = user
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var (succeeded, accessToken, refreshToken, user, errors) = await _authService.RegisterAsync(registerModel);

            if (!succeeded)
            {
                return BadRequest(new
                {
                    Succeeded = false,
                    Errors = errors
                });
            }

            return Ok(new
            {
                Succeeded = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                User = user
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenModel)
        {
            var (accessToken, refreshToken, user) = await _authService.RefreshTokenAsync(refreshTokenModel.RefreshToken);

            if (accessToken == null || refreshToken == null || user == null)
            {
                return BadRequest(new
                {
                    Succeeded = false,
                    Message = "Invalid refresh token."
                });
            }

            return Ok(new
            {
                Succeeded = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                User = user
            });
        }
    
    }

    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }
}
