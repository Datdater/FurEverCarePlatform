using System.Security.Claims;
using FurEverCarePlatform.Application.Models.Addresses;
using FurEverCarePlatform.Domain.Entities;
using FurEverCarePlatform.Persistence.DatabaseContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("api/v1/customers/{userId}/address")]
    [Authorize]
    public class CustomersController : BaseControllerApi
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly PetDatabaseContext _identityContext;

        public CustomersController(
            UserManager<AppUser> userManager,
            PetDatabaseContext identityContext
        )
        {
            _userManager = userManager;
            _identityContext = identityContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAddresses(string userId)
        {
            // Verify current user can access these addresses
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = User.IsInRole("Admin");

            if (currentUserId != userId && !isAdmin)
            {
                return Forbid();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var addresses = await _identityContext
                .Addresses.Where(a => a.AppUserId == Guid.Parse(userId))
                .ToListAsync();

            var addressResponses = addresses.Select(MapToAddressResponse).ToList();

            return Ok(addressResponses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress(string userId, string id)
        {
            // Verify current user can access this address
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = User.IsInRole("Admin");

            if (currentUserId != userId && !isAdmin)
            {
                return Forbid();
            }

            var address = await _identityContext.Addresses.FirstOrDefaultAsync(a =>
                a.AppUserId == Guid.Parse(id) && a.AppUserId == Guid.Parse(userId)
            );

            if (address == null)
            {
                return NotFound(new { Message = "Address not found" });
            }

            return Ok(MapToAddressResponse(address));
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAddress(
            string userId,
            [FromBody] AddressRequest request
        )
        {
            // Verify current user can create address for this user
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = User.IsInRole("Admin");

            if (currentUserId != userId && !isAdmin)
            {
                return Forbid();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var address = new Address
            {
                AppUserId = Guid.Parse(userId),
                Street = request.Street,
                City = request.City,
                Ward = request.Ward,
                District = request.District,
                PhoneNumber = request.PhoneNumber,
                Name = request.Name,
                IsDefault = request.IsDefault,
            };

            // If this is the first address or marked as default, ensure it's set as default
            var hasExistingAddresses = await _identityContext.Addresses.AnyAsync(a =>
                a.AppUserId == Guid.Parse(userId)
            );

            if (!hasExistingAddresses || request.IsDefault)
            {
                address.IsDefault = true;

                // If this is a new default, update other addresses
                if (hasExistingAddresses && request.IsDefault)
                {
                    var existingDefaultAddresses = await _identityContext
                        .Addresses.Where(a => a.AppUserId == Guid.Parse(userId) && a.IsDefault)
                        .ToListAsync();

                    foreach (var existingDefault in existingDefaultAddresses)
                    {
                        existingDefault.IsDefault = false;
                    }
                }
            }

            await _identityContext.Addresses.AddAsync(address);
            await _identityContext.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAddress),
                new { userId = userId, id = address.Id },
                MapToAddressResponse(address)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(
            string userId,
            string id,
            [FromBody] AddressRequest request
        )
        {
            // Verify current user can update this address
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = User.IsInRole("Admin");

            if (currentUserId != userId && !isAdmin)
            {
                return Forbid();
            }

            var address = await _identityContext.Addresses.FirstOrDefaultAsync(a =>
                a.AppUserId == Guid.Parse(id) && a.AppUserId == Guid.Parse(userId)
            );

            if (address == null)
            {
                return NotFound(new { Message = "Address not found" });
            }

            address.Street = request.Street;
            address.City = request.City;
            address.Ward = request.Ward;
            address.District = request.District;
            address.PhoneNumber = request.PhoneNumber;
            address.Name = request.Name;

            // Handle default address change if necessary
            if (request.IsDefault && !address.IsDefault)
            {
                var existingDefaultAddresses = await _identityContext
                    .Addresses.Where(a => a.AppUserId == Guid.Parse(userId) && a.IsDefault)
                    .ToListAsync();

                foreach (var existingDefault in existingDefaultAddresses)
                {
                    existingDefault.IsDefault = false;
                }

                address.IsDefault = true;
            }

            await _identityContext.SaveChangesAsync();

            return Ok(MapToAddressResponse(address));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(string userId, string id)
        {
            // Verify current user can delete this address
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = User.IsInRole("Admin");

            if (currentUserId != userId && !isAdmin)
            {
                return Forbid();
            }

            var address = await _identityContext.Addresses.FirstOrDefaultAsync(a =>
                a.AppUserId == Guid.Parse(id) && a.AppUserId == Guid.Parse(userId)
            );

            if (address == null)
            {
                return NotFound(new { Message = "Address not found" });
            }

            _identityContext.Addresses.Remove(address);

            // If the deleted address was the default one, set a new default
            if (address.IsDefault)
            {
                var newDefaultAddress = await _identityContext
                    .Addresses.Where(a =>
                        a.AppUserId == Guid.Parse(userId) && a.Id != Guid.Parse(id)
                    )
                    .FirstOrDefaultAsync();

                if (newDefaultAddress != null)
                {
                    newDefaultAddress.IsDefault = true;
                }
            }

            await _identityContext.SaveChangesAsync();

            return NoContent();
        }

        private AddressResponse MapToAddressResponse(Address address)
        {
            return new AddressResponse
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                Ward = address.Ward,
                District = address.District,
                PhoneNumber = address.PhoneNumber,
                Name = address.Name,
                IsDefault = address.IsDefault,
            };
        }
    }
}
