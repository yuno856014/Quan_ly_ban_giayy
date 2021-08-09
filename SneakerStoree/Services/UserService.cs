using Microsoft.AspNetCore.Identity;
using SneakerStoree.Entities;
using SneakerStoree.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly SignInManager<AppIdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(UserManager<AppIdentityUser> userManager,
                            SignInManager<AppIdentityUser> signInManager,
                            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public async Task<LoginResult> Login(Login LoginUser)
        {
            var user = await userManager.FindByNameAsync(LoginUser.Email);
            if (user == null)
            {
                return new LoginResult()
                {
                    UserId = string.Empty,
                    Email = string.Empty,
                    Message = "User is not existing."
                };
            }
            var signInResult = await signInManager.PasswordSignInAsync(user, LoginUser.Password, LoginUser.RememberMe, false);
            if (signInResult.Succeeded)
            {
                var roles = await userManager.GetRolesAsync(user);
                return new LoginResult()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Message = "Login successed",
                    Roles = roles.ToArray()
                };
            }
            return new LoginResult()
            {
                UserId = string.Empty,
                Email = string.Empty,
                Message = "Something went wrong, please try agin later."
            };
        }
        public async Task<RegisterResult> Register(Register register)
        {
            var registerResult = new RegisterResult();
            var newUser = new AppIdentityUser()
            {
                UserName = register.Email,
                Email = register.Email,
                NormalizedEmail = register.Email,
                NormalizedUserName = register.Email,
                LockoutEnabled = false,
                Avatar = "/images/1.png"
            };
            var user = await userManager.CreateAsync(newUser, register.Password);
            if (user.Succeeded)
            {
                var registerUser = await userManager.FindByEmailAsync(register.Email);
                var assignUserRoles = await userManager.AddToRoleAsync(newUser, "Customer");
                if (assignUserRoles.Succeeded)
                {
                    registerResult.Message = "Register succeed.";
                    registerResult.UserId = registerUser.Id;
                }

            }
            foreach (IdentityError error in user.Errors)
            {
                registerResult.Message += $"<p>{error.Description}</p>";
            }
            return registerResult;
        }

        public void Sighout()
        {
            signInManager.SignOutAsync().Wait();
        }
    }
}
