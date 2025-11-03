using Microsoft.AspNetCore.Mvc;

namespace StockExchange.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StockController : Controller
    {
        public IActionResult Select()
        {
            return View();
        }
    }
}
