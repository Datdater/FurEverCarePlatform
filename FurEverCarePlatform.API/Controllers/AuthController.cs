using FurEverCarePlatform.Application.Commons.Services;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var (accessToken, refreshToken) = await _authService.LoginAsync(
                model.EmailorPhone,
                model.Password
            );
            if (accessToken == null)
                return Unauthorized("Invalid credentials");

            // Trả về access token và refresh token (có thể gửi refresh token trong cookie HttpOnly nếu muốn)
            return Ok(new { accessToken, refreshToken });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var (succeeded, accessToken, refreshToken, errors) = await _authService.RegisterAsync(
                model
            );
            if (!succeeded)
                return BadRequest(new { errors });

            return Ok(new { accessToken, refreshToken });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var (accessToken, refreshToken) = await _authService.RefreshTokenAsync(
                request.RefreshToken
            );
            if (accessToken == null)
                return Unauthorized("Invalid refresh token");

            return Ok(new { accessToken, refreshToken });
        }
    }

    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }
}
