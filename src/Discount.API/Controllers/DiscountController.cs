using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _discountRepository.GetDiscount(productName);
            return StatusCode(200,coupon);
        }
        
        [HttpPost]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody]Coupon coupon)
        {
            var response = await _discountRepository.CreateDiscount(coupon);

            if (response == false) StatusCode(400);
            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody]Coupon coupon)
        {
            var response = await _discountRepository.UpdateDiscount(coupon);
            
            if (response == false) StatusCode(400);
            return StatusCode(204, response);
        }

        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            var response = await _discountRepository.DeleteDiscount(productName);

            if (response == false) StatusCode(404);
            return StatusCode(204);
        }
    }
}
