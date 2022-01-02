using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public Task DeleteBasket(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingCart> GetBasket(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}
