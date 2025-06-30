using System.Security.Claims;
using FurEverCarePlatform.Application.Models.Users;
using FurEverCarePlatform.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    public class UsersController : BaseControllerApi
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserManager<AppUser> userManager, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var nameParts = user.Name?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var firstName = nameParts?.FirstOrDefault() ?? "";
            var lastName = nameParts?.Length > 1 ? string.Join(' ', nameParts.Skip(1)) : "";

            return Ok(
                new UserProfileResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    ProfilePictureUrl =
                        "https://static.vecteezy.com/system/resources/thumbnails/009/292/244/small/default-avatar-icon-of-social-media-user-vector.jpg",
                    PhoneNumber = user.PhoneNumber,
                }
            );
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            user.Name = model.Name;
            user.PhoneNumber = model.PhoneNumber;
            //user.ProfilePictureUrl = model.ProfilePictureUrl;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(new { Message = $"Profile update failed: {errors}" });
            }

            return Ok(new { Message = "Profile updated successfully" });
        }
    }
}
