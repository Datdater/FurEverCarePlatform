using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Payments.Queries.GetAllTransaction
{
    public class GetAllTransactionHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllTransactionQuery, Pagination<TransactionDto>>
    {
        public async Task<Pagination<TransactionDto>> Handle(GetAllTransactionQuery request, CancellationToken cancellationToken)
        {
            var transactions = unitOfWork
                .GetRepository<Payment>()
                .GetQueryable()
                .Include(t => t.Order)
                .Include(t => t.Order.AppUser)
                .Include(t => t.Order.OrderDetails)
                .Where(t => !t.IsDeleted)
                .OrderByDescending(t => t.CreationDate)
                .Select(t => new TransactionDto
                {
                    TransactionId = t.Id,
                    Amount = t.Amount,
                    TransactionDate = t.CreationDate,
                    Status = t.PaymentStatus.ToString(),
                    UserName = t.Order.AppUser.Name,
                    ShopName = t.Order.OrderDetails.FirstOrDefault().ProductVariation.Product.Store.Name    
                });

            var response = await Pagination<TransactionDto>.CreateAsync(
                transactions,
                request.PageIndex,
                request.PageSize
            );

            return response;
        }
    }

}
