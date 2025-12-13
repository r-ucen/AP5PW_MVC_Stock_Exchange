using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;

namespace StockExchange.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        IAccountService _accountService;

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Select()
        {
            var users = _accountService.GetAllUsers().Result;
            return View(users);
        }


        public IActionResult Delete(int id)
        {
            var user = _accountService.GetUserById(id).Result;
            if (user == null)
                return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfiremd(int id)
        {
            var result = _accountService.DeleteUser(id).Result;
            if (result)
                return RedirectToAction("Select");
            else
                return BadRequest();
        }
    }
}
