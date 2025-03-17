using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Dtos;
using FurEverCarePlatform.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            try
            {
                var userDto = await _profileService.GetProfileAsync(id);
                return Ok(userDto);
            }
            catch (Exception ex) when (ex.Message == "User not found or deleted.")
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Detail = ex.Message });
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
                var userDto = await _profileService.UpdateProfileAsync(id, updatedUser);
                return Ok(userDto);
            }
            catch (Exception ex) when (ex.Message == "User not found or deleted.")
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Detail = ex.Message });
            }
        }
    }
}
