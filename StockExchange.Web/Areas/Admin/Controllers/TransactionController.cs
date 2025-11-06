using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Application.Abstraction;
using StockExchange.Infrastructure.Identity.Enums;
using StockExchange.Domain.Entities;

namespace StockExchange.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class TransactionController : Controller
    {
        ITransactionAppService _transactionService;

        public TransactionController(ITransactionAppService transactionService)
        {
            _transactionService = transactionService;
        }

        public IActionResult Select()
        {
            IList<Transaction> transactions = _transactionService.Select();
            return View(transactions);
        }
    }
}
