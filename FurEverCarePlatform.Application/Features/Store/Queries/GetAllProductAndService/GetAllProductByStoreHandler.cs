using FurEverCarePlatform.Application.Features.Products.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Store.Queries.GetAllProductAndService
{
    public class GetAllProductByStoreHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllProductByStoreQuery, Pagination<ProductDTO>>
    {
        public async Task<Pagination<ProductDTO>> Handle(GetAllProductByStoreQuery request, CancellationToken cancellationToken)
        {
            var product = unitOfWork
          .GetRepository<Domain.Entities.Product>()
          .GetQueryable()
          .Include(x => x.Store)
          .Include(x => x.Images)
          .Include(x => x.Category)
          .Where(x => !x.IsDeleted && x.StoreId == request.StoreId);
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                product = product.Where(x => x.Name.ToLower().Contains(request.SearchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                product = request.SortBy switch
                {
                    "name" => product.OrderBy(x => x.Name),
                    "price" => product.OrderBy(x => x.BasePrice),
                    _ => product.OrderBy(x => x.Name),
                };
            }

            var productRaw = await Pagination<Domain.Entities.Product>.CreateAsync(
                product,
                request.PageNumber,
                request.PageSize
            );
            var productDTOs = mapper.Map<Pagination<ProductDTO>>(productRaw);
            return productDTOs;
        }
    }
}
