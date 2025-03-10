using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FurEverCarePlatform.Application.Exception;

namespace FurEverCarePlatform.Application.Features.ProductCategory.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateProductCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateProductCategoryValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.ToString(), validationResult);
            }
            var productCategory = _mapper.Map<Domain.Entities.ProductCategory>(request);
            await _unitOfWork.CategoryRepository.InsertAsync(productCategory);
            await _unitOfWork.SaveAsync();
            return productCategory.Id;
        }
    }
}
