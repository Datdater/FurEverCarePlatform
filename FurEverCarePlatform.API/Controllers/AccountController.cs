using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FurEverCarePlatform.Domain.Entities;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Services;
using FurEverCarePlatform.Application.Models;
using Microsoft.AspNetCore.Authorization;
namespace FurEverCarePlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtTokenService _jwtTokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser { UserName = model.Email, Email = model.Email, Name = model.Name };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Đăng nhập cookie (nếu cần cho web)
                await _signInManager.SignInAsync(user, isPersistent: false);
                // Tạo JWT cho API
                var token = await _jwtTokenService.GenerateToken(user);
                return Ok(new { message = "Đăng ký thành công!", token });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")] // Hoặc IdentityConstants.ApplicationScheme cho cookie
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new { message = "Email không tồn tại." });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var token = await _jwtTokenService.GenerateToken(user);
                return Ok(new { message = "Đăng nhập thành công!", token });
            }

            return BadRequest(new { message = "Đăng nhập thất bại." });
        }

        [HttpGet("protected")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetProtectedData()
        {
            return Ok(new { message = "Dữ liệu bảo mật từ API!" });
        }
    }
}