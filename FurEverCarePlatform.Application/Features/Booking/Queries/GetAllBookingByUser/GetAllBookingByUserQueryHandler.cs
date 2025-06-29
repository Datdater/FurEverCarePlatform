using FurEverCarePlatform.Application.Features.Booking.DTOs;
using FurEverCarePlatform.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Booking.Queries.GetAllBookingByUser;

public class GetAllBookingByUserQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllBookingByUserQuery, Pagination<GetAllBookingDto>>
{
    public async Task<Pagination<GetAllBookingDto>> Handle(GetAllBookingByUserQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.GetRepository<Domain.Entities.Booking>().GetQueryable()
            .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.Pet)
            .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.PetServiceDetail)
                    .ThenInclude(psd => psd.PetService)
            .Include(b => b.AppUser)
            .Include(b => b.Store)
            .Where(b => (!request.AppUserId.HasValue || b.AppUserId == request.AppUserId.Value) &&
                        (!request.StoreId.HasValue || b.StoreId == request.StoreId.Value));

        var bookingsPage = await Pagination<Domain.Entities.Booking>.CreateAsync(
            query,
            request.PageIndex,
            request.PageSize);

        var bookingDtos = new List<GetAllBookingDto>();

        foreach (var booking in bookingsPage.Items)
        {
            var petServiceGroups = booking.BookingDetails
                .GroupBy(bd => bd.PetId)
                .ToList();

            var petWithServicesList = new List<PetWithServices>();

            foreach (var petGroup in petServiceGroups)
            {
                var petId = petGroup.Key;

                var firstBookingDetail = petGroup.First();
                if (firstBookingDetail.Pet == null) continue;

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
                        services.Add(new Service
                        {
                            Id = bookingDetail.PetServiceDetailId,
                            ServiceDetailName = bookingDetail.PetServiceDetail.Name,
                            Price = bookingDetail.RawAmount,
                        });
                    }
                }

                petWithServicesList.Add(new PetWithServices
                {
                    Pet = pet,
                    Services = services.Distinct().ToList()
                });
            }

            var bookingDto = new GetAllBookingDto
            {
                BookingId = booking.Id,
                ShopName = booking.Store.Name,
                UserName = booking.AppUser?.Name!,
                UserPhone = booking.AppUser?.PhoneNumber ?? string.Empty,
                Status = booking.Status,
                TotalPrice = booking.TotalAmount,
                BookingTime = booking.BookingTime,
                PetWithServices = petWithServicesList
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