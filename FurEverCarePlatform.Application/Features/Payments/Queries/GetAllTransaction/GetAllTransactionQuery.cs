using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Payments.Queries.GetAllTransaction
{
    public class GetAllTransactionQuery : IRequest<Pagination<TransactionDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class TransactionDto
    {
        public Guid TransactionId { get; set; }
        public float Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }

        public string ShopName { get; set; }    
    }
}
