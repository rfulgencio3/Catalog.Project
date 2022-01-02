using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    public class DiscountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
