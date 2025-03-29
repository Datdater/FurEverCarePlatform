using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Dtos;
using Microsoft.AspNetCore.Identity;

namespace FurEverCarePlatform.Application.Commons.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClaimService _claimService;
        private readonly UserManager<AppUser> _userManager;
        public ProfileService(IUserRepository userRepository, IClaimService claimService, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _claimService = claimService;
            _userManager = userManager;
        }

        public async Task<AppUserDto> GetProfileAsync()
        {
            var user = await _userRepository.GetByIdWithRelatedDataAsync(_claimService.GetCurrentUser);
            if (user == null || user.IsDeleted)
            {
                throw new System.Exception("User not found or deleted.");
            }

            return MapToDto(user);
        }

        public async Task<AppUserDto> UpdatePassWord(Guid userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdWithRelatedDataAsync(userId);
            if (user == null || user.IsDeleted)
            {
                throw new System.Exception("User not found or deleted.");
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, oldPassword);
            if (!passwordValid)
            {
                throw new System.Exception("Old password is incorrect.");
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new System.Exception($"Failed to update password: {errors}");
            }

            user.ModificationDate = DateTime.Now;
            user.ModifiedBy = userId;
            await _userRepository.UpdateAsync(user);

            return MapToDto(user);
        }

        public async Task<AppUserDto> UpdateProfileAsync(Guid userId, AppUserDto updatedUser)
        {
            var existingUser = await _userRepository.GetByIdWithRelatedDataAsync(userId);
            if (existingUser == null || existingUser.IsDeleted)
            {
                throw new System.Exception("User not found or deleted.");
            }

            existingUser.Name = updatedUser.Name;
            existingUser.UserName = updatedUser.Name;
            existingUser.PhoneNumber = updatedUser.PhoneNumber;
            existingUser.ModificationDate = DateTime.Now;
            existingUser.ModifiedBy = userId;

            await _userRepository.UpdateAsync(existingUser);
            return MapToDto(existingUser);
        }

        private AppUserDto MapToDto(AppUser user)
        {
            return new AppUserDto
            {
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };
        }
    }
}
