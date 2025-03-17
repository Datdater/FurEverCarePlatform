using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Features.ProductCategory.Commands.CreateProductCategory;
using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService
{
    public class CreatePetServiceCommandHandler : IRequestHandler<CreatePetServiceCommand, Guid>
	{
        private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        private readonly ILogger<CreatePetServiceCommandHandler> _logger;
        public CreatePetServiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreatePetServiceCommandHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Guid> Handle(CreatePetServiceCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Bắt đầu xử lý yêu cầu tạo PetService: {@Request}", request);

            // Validate request
            var validator = new CreatePetServiceValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Yêu cầu không hợp lệ: {Errors}", validationResult.Errors);
                throw new BadRequestException("Dữ liệu không hợp lệ", validationResult);
            }

            // Kiểm tra khóa ngoại
           

            // Ánh xạ từ Command sang Entity
            var petService = _mapper.Map<Domain.Entities.PetService>(request);
            _logger.LogInformation("PetService sau khi ánh xạ: {@PetService}", petService);

            // Xử lý PetServiceDetails
            if (request.PetServiceDetails != null && request.PetServiceDetails.Any())
            {
                petService.PetServiceDetails = _mapper.Map<List<PetServiceDetail>>(request.PetServiceDetails);
                foreach (var detail in petService.PetServiceDetails)
                {
                    detail.PetServiceId = petService.Id; // Gán khóa ngoại
                    _logger.LogInformation("PetServiceDetail sau ánh xạ: {@Detail}", detail);
                }
            }

            // Thêm vào repository
            await _unitOfWork.GetRepository<Domain.Entities.PetService>().InsertAsync(petService);

            // Lưu thay đổi
            try
            {
                await _unitOfWork.SaveAsync();
                _logger.LogInformation("PetService đã được tạo với Id: {Id}", petService.Id);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Lỗi khi lưu PetService: {InnerException}", ex.InnerException?.Message);
                throw;
            }

            return petService.Id;
        }
        //public async Task<Guid> Handle(CreatePetServiceCommand request, CancellationToken cancellationToken)
        //{
        //    var validator = new CreatePetServiceValidator();
        //    var validationResult = await validator.ValidateAsync(request);
        //    if (!validationResult.IsValid)
        //    {
        //        throw new BadRequestException(validationResult.ToString(), validationResult);
        //    }
        //    var petService = _mapper.Map<Domain.Entities.PetService>(request);
        //    if (request.PetServiceDetails != null)
        //    {
        //        petService.PetServiceDetails = _mapper.Map<List<Domain.Entities.PetServiceDetail>>(request.PetServiceDetails);
        //    }

        //    await _unitOfWork.GetRepository<Domain.Entities.PetService>().InsertAsync(petService);
        //    await _unitOfWork.SaveAsync();

        //    // Gán PetServiceId cho PetServiceDetails nếu cần
        //    if (petService.PetServiceDetails != null)
        //    {
        //        foreach (var detail in petService.PetServiceDetails)
        //        {
        //            detail.PetServiceId = petService.Id;
        //        }
        //        await _unitOfWork.SaveAsync(); // Lưu lại nếu có thay đổi
        //    }
        //    return petService.Id;
        //}
        //public async Task<Guid> Handle(CreatePetServiceCommand request, CancellationToken cancellationToken)
        //{
        //    var petService = new Domain.Entities.PetService
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = request.Name,
        //        Description = request.Description,
        //        StoreId = request.StoreId,
        //        ServiceCategoryId = request.ServiceCategoryId,
        //        EstimatedTime = request.EstimatedTime
        //    };

        //    _unitOfWork.GetRepository<Domain.Entities.PetService>().InsertAsync(petService);
        //    await _unitOfWork.SaveAsync();

        //    return petService.Id;
        //}
    }
}
