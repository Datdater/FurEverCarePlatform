using FurEverCarePlatform.Application.Features.PetService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetAllPetService
{
    //public class GetAllPetServiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    //: IRequestHandler<GetAllPetServiceQuery, Pagination<PetServiceDto>>
    //{
    //    public async Task<Pagination<PetServiceDto>> Handle(GetAllPetServiceQuery request, CancellationToken cancellationToken)
    //    {
    //        // Lấy dữ liệu PetService với phân trang và các quan hệ liên quan
    //        var petServiceRaw = await unitOfWork.GetRepository<Domain.Entities.PetService>()
    //            .GetPaginationAsync("Store,ServiceCategory", request.PageNumber, request.PageSize);

    //        // Ánh xạ dữ liệu thô sang DTO
    //        var petServiceDTOs = mapper.Map<Pagination<PetServiceDto>>(petServiceRaw);

    //        // Tùy chỉnh các thuộc tính của DTO nếu cần (tương tự cách GetAllProductHandler làm với MinPrices)
    //        var petServiceDTOItems = petServiceDTOs.Items.Select(ps => new PetServiceDto
    //        {
    //            Id = ps.Id,
    //            Name = ps.Name,
    //            Description = ps.Description,
    //            StoreId = ps.StoreId,
    //            StoreName = ps.Store?.Name, // Lấy tên từ Store (nếu có)
    //            ServiceCategoryId = ps.ServiceCategoryId,
    //            ServiceCategoryName = ps.ServiceCategory?.Name, // Lấy tên từ ServiceCategory (nếu có)
    //            EstimatedTime = ps.EstimatedTime
    //        }).ToList();

    //        // Cập nhật lại Items trong Pagination
    //        petServiceDTOs.Items = petServiceDTOItems;

    //        return petServiceDTOs;
    //    }
    //}
    public class GetAllPetServiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetAllPetServiceQuery, Pagination<PetServiceDto>>
    {
        public async Task<Pagination<PetServiceDto>> Handle(GetAllPetServiceQuery request, CancellationToken cancellationToken)
        {
            // Lấy dữ liệu PetService với phân trang và các quan hệ liên quan
            var petServiceRaw = await unitOfWork.GetRepository<Domain.Entities.PetService>()
                .GetPaginationAsync("Store,ServiceCategory", request.PageNumber, request.PageSize);

            // Tùy chỉnh các thuộc tính của DTO
            var petServiceDTOItems = petServiceRaw.Items?.Select(ps => new PetServiceDto
            {
                Id = ps.Id,
                Name = ps.Name,
                Description = ps.Description,
                StoreId = ps.StoreId,
                //StoreName = ps.Store?.Name ?? string.Empty,
                ServiceCategoryId = ps.ServiceCategoryId,
               // ServiceCategoryName = ps.ServiceCategory?.Name ?? string.Empty,
                EstimatedTime = ps.EstimatedTime
            })?.ToList() ?? new List<PetServiceDto>();

            // Tạo đối tượng Pagination mới với dữ liệu đã ánh xạ
            var petServiceDTOs = new Pagination<PetServiceDto>
            {
                TotalItemsCount = petServiceRaw.TotalItemsCount,
                PageSize = petServiceRaw.PageSize,
                PageIndex = petServiceRaw.PageIndex,
                Items = petServiceDTOItems
            };

            return petServiceDTOs;
        }
    }
}
