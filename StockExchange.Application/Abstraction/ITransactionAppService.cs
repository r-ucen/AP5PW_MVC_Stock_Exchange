using StockExchange.Application.ViewModels;
using StockExchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.Abstraction
{
    public interface ITransactionAppService
    {
        IList<Transaction> Select();
        Task<IList<TransactionViewModel>> SelectByUserIdAsync(int userId);
    }
}
