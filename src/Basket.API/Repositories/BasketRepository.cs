using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var shoppingCart = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(shoppingCart)) return null;
            return JsonSerializer.Deserialize<ShoppingCart>(shoppingCart);
        }
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            await _redisCache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart));

            return await GetBasket(shoppingCart.UserName);
        }
        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
