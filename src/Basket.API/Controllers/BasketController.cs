using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : Controller
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var shoppingCart = await _basketRepository.GetBasket(userName);

            return StatusCode(200, shoppingCart ?? new ShoppingCart(userName)); 
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            var response = await _basketRepository.UpdateBasket(shoppingCart);
            return StatusCode(200, response);
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return StatusCode(204);
        }
    }
}
