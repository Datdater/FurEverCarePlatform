using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Models;

namespace FurEverCarePlatform.Application.Contracts
{
    public interface ICartRepository
    {
        Task<ShoppingCart> GetCartAsync(string userId);
        Task UpdateCartAsync(ShoppingCart cart);
        Task DeleteCartAsync(string userId);
    }
}
