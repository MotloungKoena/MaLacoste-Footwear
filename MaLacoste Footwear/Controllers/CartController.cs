using Microsoft.AspNetCore.Mvc;

namespace MaLacoste_Footwear.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
