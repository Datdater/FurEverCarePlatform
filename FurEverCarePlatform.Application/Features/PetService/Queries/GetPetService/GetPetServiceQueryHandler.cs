using FurEverCarePlatform.Application.Features.PetService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService
{
    //public class GetPetServiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    //: IRequestHandler<GetPetServiceQuery, PetServiceDto>
    //{
    //    public async Task<PetServiceDto> Handle(GetPetServiceQuery request, CancellationToken cancellationToken)
    //    {
    //        // Lấy thông tin PetService với các quan hệ Store và ServiceCategory
    //        var petService = await unitOfWork.PetServiceRepository
    //            .GetFirstOrDefaultAsync(x => x.Id == request.Id, "Store,ServiceCategory");

    //        if (petService == null)
    //        {
    //            throw new NotFoundException(nameof(Domain.Entities.PetService), request.Id);
    //        }

    //        // Ánh xạ PetService sang DTO
    //        var petServiceDto = mapper.Map<PetServiceDto>(petService);

    //        // Lấy danh sách PetServiceSteps
    //        var petServiceStepsRaw = await unitOfWork.GetRepository<Domain.Entities.PetServiceStep>()
    //            .GetAllAsync(x => x.PetServiceId == request.Id, "Pet Service Step");
    //        var petServiceSteps = mapper.Map<List<PetServiceStepDto>>(petServiceStepsRaw);
    //        petServiceDto.PetServiceSteps = petServiceSteps;

    //        // Lấy danh sách ComboServices
    //        var comboServicesRaw = await unitOfWork.GetRepository<Domain.Entities.ComboService>()
    //            .GetAllAsync(x => x.PetServiceId == request.Id);
    //        var comboServices = mapper.Map<List<ComboServiceDto>>(comboServicesRaw);
    //        petServiceDto.ComboServices = comboServices;

    //        return petServiceDto;
    //    }
    //}

    public class GetPetServiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetPetServiceQuery, PetServiceDto>
    {
        public async Task<PetServiceDto> Handle(GetPetServiceQuery request, CancellationToken cancellationToken)
        {
            // Lấy thông tin PetService với các quan hệ Store và ServiceCategory
            var petService = await unitOfWork.GetRepository<Domain.Entities.PetService>()
                .GetFirstOrDefaultAsync(x => x.Id == request.Id, "Store,ServiceCategory");

            if (petService == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.PetService), request.Id);
            }

            // Ánh xạ PetService sang DTO
            var petServiceDto = mapper.Map<PetServiceDto>(petService);

            // Lấy danh sách PetServiceSteps
            var petServiceStepsRaw = await unitOfWork.GetRepository<Domain.Entities.PetServiceStep>()
                .GetAllAsync(x => x.PetServiceId == request.Id, "PetService");
            var petServiceSteps = mapper.Map<List<PetServiceStepDto>>(petServiceStepsRaw);
            petServiceDto.PetServiceSteps = petServiceSteps;

            // Lấy danh sách PetServiceDetails
            var petServiceDetailsRaw = await unitOfWork.GetRepository<Domain.Entities.PetServiceDetail>()
                .GetAllAsync(x => x.PetServiceId == request.Id, "PetService");
            var petServiceDetails = mapper.Map<List<PetServiceDetailDto>>(petServiceDetailsRaw);
            petServiceDto.PetServiceDetails = petServiceDetails;

            // Lấy danh sách ComboServices
            var comboServicesRaw = await unitOfWork.GetRepository<Domain.Entities.ComboService>()
                .GetAllAsync(x => x.PetServiceId == request.Id, "PetService");
            var comboServices = mapper.Map<List<ComboServiceDto>>(comboServicesRaw);

            // Lấy thông tin ComboName từ quan hệ Combo (nếu cần)
            foreach (var comboService in comboServices)
            {
                var combo = await unitOfWork.GetRepository<Domain.Entities.Combo>()
                    .GetByIdAsync(comboService.ComboId);
                comboService.ComboName = combo?.Name ?? string.Empty; // Gán tên Combo nếu tồn tại
            }

            petServiceDto.ComboServices = comboServices;

            return petServiceDto;
        }
    }
}
