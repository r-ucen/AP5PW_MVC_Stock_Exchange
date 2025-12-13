using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockExchange.Application.Abstraction;
using StockExchange.Application.ViewModels;
using StockExchange.Infrastructure.Identity;
using StockExchange.Infrastructure.Identity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.Implementation
{
    public class AccountIdentityService : IAccountService
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;

        public AccountIdentityService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IList<UserViewModel>> GetAllUsers()
        {
            var users = await userManager.Users.ToArrayAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (User user in users)
            {
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsDisabled = user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.UtcNow,
                    Roles = await userManager.GetRolesAsync(user)
                });
            }

            return userViewModels;
        }

        public async Task<UserViewModel> GetUserById(int id)
        {
            var user = await userManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            if (user == null)
                return null;

            UserViewModel userViewModel = new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsDisabled = user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.UtcNow,
                Roles = userManager.GetRolesAsync(user).Result
            };

            return userViewModel;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return false;

            var result = await userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DisableUser(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return false;
            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(1000);
            var result = await userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> EnableUser(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return false;
            user.LockoutEnd = null;
            var result = await userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> Login(LoginViewModel vm)
        {
            var result = await signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, true);
            return result.Succeeded;
        }

        public Task Logout()
        {
            return signInManager.SignOutAsync();
        }


        public async Task<string[]> Register(RegisterViewModel vm, params Roles[] roles)
        {
            User user = new User()
            {
                UserName = vm.Username,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                PhoneNumber = vm.Phone
            };

            string[] errors = null;

            var result = await userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                foreach (var role in roles)
                {
                    var resultRole = await userManager.AddToRoleAsync(user, role.ToString());

                    if (resultRole.Succeeded == false)
                    {
                        for (int i = 0; i < result.Errors.Count(); ++i)
                            result.Errors.Append(result.Errors.ElementAt(i));
                    }
                }
            }

            if (result.Errors != null && result.Errors.Count() > 0)
            {
                errors = new string[result.Errors.Count()];
                for (int i = 0; i < result.Errors.Count(); ++i)
                {
                    errors[i] = result.Errors.ElementAt(i).Description;
                }
            }

            return errors;
        }
    }
}