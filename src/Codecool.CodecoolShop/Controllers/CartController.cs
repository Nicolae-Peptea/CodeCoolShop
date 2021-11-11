using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Codecool.CodecoolShop.Controllers
{
    public class CartController : Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
            Log.Information("User initialized checkout process");
            decimal result;
            decimal.TryParse(HttpContext.Request.Form["total-value"], out result);
            ViewBag.TotalCart = result * 100;
            return View();
        }
    }
}
