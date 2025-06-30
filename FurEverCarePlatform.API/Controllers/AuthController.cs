using System.ComponentModel.DataAnnotations;
using System.Web;
using Azure.Core;
using FurEverCarePlatform.API.Models;
using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Commons.Services;
using FurEverCarePlatform.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : BaseControllerApi
    {
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly EmailService _emailService;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(
            AuthService authService,
            ILogger<AuthController> logger,
            EmailService emailService,
            UserManager<AppUser> userManager
        )
        {
            _authService = authService;
            _logger = logger;
            _emailService = emailService;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(model.Email, model.Password);

            if (result.Success)
            {
                return Ok(result.Response);
            }

            return Unauthorized(new { Message = result.Message });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(registerDto);

            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(new { Message = result.Message });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return BadRequest(new { Message = "Refresh token is required" });
            }

            var result = await _authService.RefreshTokenAsync(request.RefreshToken);

            if (result != null)
            {
                return Ok(result);
            }

            return Unauthorized(new { Message = "Refresh token is invalid" });
        }

        [HttpPost("email")]
        public async Task<IActionResult> SendConfirmationEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("User not found");

            if (user.EmailConfirmed)
                return BadRequest("Email already confirmed");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);
            var confirmationLink =
                "https://senandpet.vercel.app/email-confirm?email={email}&token={encodedToken}";
            var message =
                $@"
                <h1>Confirm your email</h1>
                <p>Please confirm your email by clicking the link below:</p>
                <a href='{confirmationLink}'>Confirm Email</a>
            ";

            await _emailService.SendEmailAsync(email, "Confirm your email", message);
            return Ok("Confirmation email sent");
        }

        [HttpGet("email-confirm")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("User not found");

            if (user.EmailConfirmed)
                return BadRequest("Email already confirmed");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return Ok("Email confirmed successfully");

            return BadRequest("Failed to confirm email");
        }
    }
}

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; }
}

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
