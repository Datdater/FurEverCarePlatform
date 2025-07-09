using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Features.Booking.DTOs;
using FurEverCarePlatform.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.Application.Features.Booking.Queries.GetAllBookingByUser;

public class GetAllBookingByUserQueryHandler(
    IUnitOfWork unitOfWork,
    IClaimService claimService,
    UserManager<AppUser> userManager
) : IRequestHandler<GetAllBookingByUserQuery, Pagination<GetAllBookingDto>>
{
    public async Task<Pagination<GetAllBookingDto>> Handle(
        GetAllBookingByUserQuery request,
        CancellationToken cancellationToken
    )
    {
        var userId = claimService.GetCurrentUser;
        var user = await userManager.FindByIdAsync(userId.ToString());

        var roles = await userManager.GetRolesAsync(user);

        // Build the base query with all includes
        var baseQuery = unitOfWork
            .GetRepository<Domain.Entities.Booking>()
            .GetQueryable()
            .Include(b => b.BookingDetails)
            .ThenInclude(bd => bd.Pet)
            .Include(b => b.BookingDetails)
            .ThenInclude(bd => bd.PetServiceDetail)
            .ThenInclude(psd => psd.PetService)
            .Include(b => b.AppUser)
            .Include(b => b.Store).Where(b => request.Status == null || b.Status == request.Status);

        // Apply filtering based on role - use IQueryable instead of casting
        IQueryable<Domain.Entities.Booking> filteredQuery;

        if (roles.Contains("Store Owner"))
        {
            var store = await unitOfWork
                .GetRepository<Domain.Entities.Store>()
                .GetQueryable()
                .FirstOrDefaultAsync(s => s.AppUserId == userId);

            if (store != null)
            {
                filteredQuery = baseQuery.Where(b => b.StoreId == store.Id);
            }
            else
            {
                filteredQuery = baseQuery.Where(b => false); // No store found, return empty result
            }
        }
        else
        {
            filteredQuery = baseQuery.Where(b => b.AppUserId == userId);
        }

        var bookingsPage = await Pagination<Domain.Entities.Booking>.CreateAsync(
            filteredQuery,
            request.PageIndex,
            request.PageSize
        );

        var bookingDtos = new List<GetAllBookingDto>();

        foreach (var booking in bookingsPage.Items)
        {
            var petServiceGroups = booking.BookingDetails.GroupBy(bd => bd.PetId).ToList();

            var petWithServicesList = new List<PetWithServices>();

            foreach (var petGroup in petServiceGroups)
            {
                var petId = petGroup.Key;

                var firstBookingDetail = petGroup.First();
                if (firstBookingDetail.Pet == null)
                    continue;

                var pet = new DTOs.Pet
                {
                    Id = petId,
                    Name = firstBookingDetail.Pet.Name,
                    PetType = firstBookingDetail.Pet.PetType,
                    Color = firstBookingDetail.Pet.Color,
                    Dob = firstBookingDetail.Pet.Dob,
                };

                var services = new List<Service>();
                foreach (var bookingDetail in petGroup)
                {
                    if (bookingDetail.PetServiceDetail != null)
                    {
                        services.Add(
                            new Service
                            {
                                Id = bookingDetail.PetServiceDetailId,
                                ServiceDetailName = bookingDetail.PetServiceDetail.Name,
                                Price = bookingDetail.RawAmount,
                                ImageUrl = bookingDetail.PetServiceDetail.Image ?? string.Empty,
                            }
                        );
                    }
                }

                petWithServicesList.Add(
                    new PetWithServices { Pet = pet, Services = services.Distinct().ToList() }
                );
            }

            var bookingDto = new GetAllBookingDto
            {
                BookingId = booking.Id,
                ShopName = booking.Store.Name,
                StoreAddressDistrict = booking.Store.BusinessAddressDistrict ?? string.Empty,
                StoreAddressProvince = booking.Store.BusinessAddressProvince ?? string.Empty,
                StoreAddressWard = booking.Store.BusinessAddressWard ?? string.Empty,
                StoreAddressStreet = booking.Store.BusinessAddressStreet ?? string.Empty,
                UserName = booking.AppUser?.Name!,
                UserPhone = booking.AppUser?.PhoneNumber ?? string.Empty,
                Status = booking.Status,
                TotalPrice = booking.TotalAmount,
                BookingTime = booking.BookingTime,
                PetWithServices = petWithServicesList,
            };

            bookingDtos.Add(bookingDto);
        }

        var bookingDtoPagination = Pagination<GetAllBookingDto>.Create(
            bookingDtos,
            request.PageIndex,
            request.PageSize,
            bookingDtos.Count
        );

        return bookingDtoPagination;
    }
}
