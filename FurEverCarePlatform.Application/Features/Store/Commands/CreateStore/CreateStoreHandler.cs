using Microsoft.AspNetCore.Identity;

namespace FurEverCarePlatform.Application.Features.Store.Commands.CreateStore;

public class CreateStoreHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    : IRequestHandler<CreateStoreCommand, Guid>
{
    public async Task<Guid> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateStoreValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult.ToString(), validationResult);
        }

        try
        {
            await unitOfWork.BeginTransactionAsync();

            var existingUser = await userManager.FindByEmailAsync(request.FaxEmail);
            if (existingUser != null)
            {
                throw new BadRequestException("User with this email already exists");
            }

            var user = new AppUser
            {
                UserName = request.Username,
                Email = request.FaxEmail,
                PhoneNumber = request.Hotline,
                Name = request.Name,
                CreationDate = DateTime.UtcNow.AddHours(7)
            };

            var createResult = await userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new BadRequestException($"User creation failed: {errors}");
            }

            var roleResult = await userManager.AddToRoleAsync(user, "Store Owner");
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                throw new BadRequestException($"Role assignment failed: {errors}");
            }

            var store = new Domain.Entities.Store()
            {
                AppUser = user,
                Name = request.Name,
                Hotline = request.Hotline,
                LogoUrl = request.LogoUrl,
                BannerUrl = request.BannerUrl,
                BusinessType = request.BusinessType,
                BusinessAddressProvince = request.BusinessAddressProvince,
                BusinessAddressDistrict = request.BusinessAddressDistrict,
                BusinessAddressWard = request.BusinessAddressWard,
                BusinessAddressStreet = request.BusinessAddressStreet,
                FaxEmail = request.FaxEmail,
                FaxCode = request.FaxCode,
                FrontIdentityCardUrl = request.FrontIdentityCardUrl,
                BackIdentityCardUrl = request.BackIdentityCardUrl,
                Wallet = new Wallet
                {
                    Price = 0,
                }
            };

            var newStore = await unitOfWork
                .GetRepository<Domain.Entities.Store>()
                .InsertAsync(store);

            await unitOfWork.SaveAsync();
            await unitOfWork.CommitTransactionAsync();

            return newStore.Id;
        }
        catch (System.Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();

            if (ex is BadRequestException)
            {
                throw;
            }

            throw new BadRequestException($"Error in creating store: {ex.Message}");
        }
    }
}