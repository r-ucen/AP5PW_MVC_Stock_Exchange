using Microsoft.AspNetCore.Identity;
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
        Task<IList<UserViewModel>> GetAllUsers();
        Task<UserViewModel> GetUserById(int id);
        Task<bool> DeleteUser(int id);
        Task<bool> DisableUser(int id);
        Task<bool> EnableUser(int id);
        Task<EditUserRolesViewModel> GetUserRolesForEdit(int id);
        Task<bool> UpdateUserRoles(int userId, IList<string> selectedRoles);
    }
}
