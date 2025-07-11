using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Orders.Commands.Update
{
    public class UpdateOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateOrderCommand>
    {
        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await unitOfWork.GetRepository<Order>().GetQueryable().Include(x => x.OrderDetails)
                    .ThenInclude(x => x.ProductVariation.Product.Store.Wallet).FirstOrDefaultAsync(x => x.Id == request.OrderId);

            if (order == null)
                throw new NotFoundException("Order", request.OrderId);

            order.OrderStatus = request.EnumOrderStatus;
            if (request.EnumOrderStatus == Domain.Enums.EnumOrderStatus.Delivered)
            {
                order.OrderCompletedAt = DateTime.UtcNow.AddHours(7);
                var storeWallet = order.OrderDetails.FirstOrDefault()?.ProductVariation.Product.Store.Wallet;
                if (storeWallet != null)
                {
                    storeWallet.Price += order.TotalPrice;

                    unitOfWork.GetRepository<Domain.Entities.Wallet>().Update(storeWallet);
                }
            }

            unitOfWork.GetRepository<Order>().Update(order);

            await unitOfWork.SaveAsync();
        }
    }
}
