using StockExchange.Application.ViewModels;
using StockExchange.Infrastructure.Identity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.Abstraction
{
    public interface IAccountService
    {
        Task<bool> Login(LoginViewModel vm);
        Task Logout();
        Task<string[]> Register(RegisterViewModel vm, params Roles[] roles);
    }
}
