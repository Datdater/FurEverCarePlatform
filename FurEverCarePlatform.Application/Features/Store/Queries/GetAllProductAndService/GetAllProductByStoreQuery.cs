using FurEverCarePlatform.Application.Features.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Store.Queries.GetAllProductAndService
{
    public class GetAllProductByStoreQuery : IRequest<Pagination<ProductDTO>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        public Guid StoreId { get; set; }
        public string? SearchTerm { get; set; } = null;
        public string? SortBy { get; set; } = "Name";
    }
}
