using System.Security.Claims;
using FurEverCarePlatform.API.Models;
using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Commons.Services;
using FurEverCarePlatform.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class ProfileController(IProfileService profileService, IClaimService claimService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userDto = await profileService.GetProfileAsync();
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
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto updatePasswordDto)
        {
            try
            {
                // Lấy ID của người dùng hiện tại từ token
                var userId = claimService.GetCurrentUser;

                // Gọi ProfileService để cập nhật mật khẩu
                var updatedUser = await profileService.UpdatePassWord(userId, updatePasswordDto.OldPassword, updatePasswordDto.NewPassword);

                return Ok(new
                {
                    Succeeded = true,
                    Message = "Password updated successfully.",
                    Data = updatedUser
                });
            }
            catch (Exception ex)
            {
                // Không log thông tin nhạy cảm
                return BadRequest(new
                {
                    Succeeded = false,
                    Message = ex.Message
                });
            }
        }
    }
}
