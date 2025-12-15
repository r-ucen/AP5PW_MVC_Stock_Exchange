using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Infrastructure.Identity.Enums;

namespace StockExchange.Web.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Select()
        {
            return View();
        }
    }
}
