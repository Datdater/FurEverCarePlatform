using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Dtos;

namespace FurEverCarePlatform.Application.Commons.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClaimService _claimService;

        public ProfileService(IUserRepository userRepository, IClaimService claimService)
        {
            _userRepository = userRepository;
            _claimService = claimService;
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

        public async Task<AppUserDto> UpdateProfileAsync(Guid userId, AppUserDto updatedUser)
        {
            var existingUser = await _userRepository.GetByIdWithRelatedDataAsync(userId);
            if (existingUser == null || existingUser.IsDeleted)
            {
                throw new System.Exception("User not found or deleted.");
            }

            existingUser.Name = updatedUser.Name;
            existingUser.Address = updatedUser.Address;
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
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreationDate = user.CreationDate,
                CreatedBy = user.CreatedBy,
                ModificationDate = user.ModificationDate,
                ModifiedBy = user.ModifiedBy,
                DeleteDate = user.DeleteDate,
                DeletedBy = user.DeletedBy,
                IsDeleted = user.IsDeleted,
                Orders = user.Orders?.ToList() ?? new List<Order>(),
                Bookings = user.Bookings?.ToList() ?? new List<Booking>(),
                Pets = user.Pets?.ToList() ?? new List<Pet>(),
                Address = user.Address?.ToList() ?? new List<Address>(),
                Notifications = user.Notifications?.ToList() ?? new List<Notification>(),
                Feedback = user.Feedback?.ToList() ?? new List<Feedback>(),
                ChatMessage = user.ChatMessage?.ToList() ?? new List<ChatMessage>(),
                Reports = user.Reports?.ToList() ?? new List<Report>(),
            };
        }
    }
}
