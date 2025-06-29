using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FurEverCarePlatform.Persistence.Repositories
{
    public class RedisCartRepository : ICartRepository
    {
        private readonly IDistributedCache _redisCache;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromDays(30);

        public RedisCartRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task UpdateCartAsync(ShoppingCart cart)
        {
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(
                _cacheExpiration
            );

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver(),
            };

            var cartJson = JsonConvert.SerializeObject(cart, jsonSettings);
            await _redisCache.SetStringAsync(cart.UserId, cartJson, options);
        }

        public async Task<ShoppingCart> GetCartAsync(string userId)
        {
            var cartJson = await _redisCache.GetStringAsync(userId);

            if (string.IsNullOrEmpty(cartJson))
                return new ShoppingCart(userId);

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver(),
            };

            return JsonConvert.DeserializeObject<ShoppingCart>(cartJson, jsonSettings)
                ?? new ShoppingCart(userId);
        }

        public async Task DeleteCartAsync(string userId)
        {
            await _redisCache.RemoveAsync(userId);
        }
    }
}

public class PrivateSetterContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(
        MemberInfo member,
        MemberSerialization memberSerialization
    )
    {
        var prop = base.CreateProperty(member, memberSerialization);

        if (prop.Writable)
            return prop;

        var property = member as PropertyInfo;
        if (property != null)
        {
            var hasPrivateSetter = property.GetSetMethod(true) != null;
            prop.Writable = hasPrivateSetter;
        }

        return prop;
    }
}
