using System.Security.Claims;
using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class ProfileController(IProfileService profileService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var user = HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                var userDto = await profileService.GetProfileAsync(Guid.Parse(userId));
                return Ok(userDto);
            }
            catch (Exception ex) when (ex.Message == "User not found or deleted.")
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new { Message = "An unexpected error occurred.", Detail = ex.Message }
                );
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] AppUserDto updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest(new { Message = "User ID mismatch." });
            }

            try
            {
                var userDto = await profileService.UpdateProfileAsync(id, updatedUser);
                return Ok(userDto);
            }
            catch (Exception ex) when (ex.Message == "User not found or deleted.")
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new { Message = "An unexpected error occurred.", Detail = ex.Message }
                );
            }
        }
    }
}
