using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> Login(LoginDto model)
        {
            var res = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: true);
            return res.Succeeded;
        }

        public async Task<IdentityResult> Register(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
                PhoneNumber = model.Phone?.ToString(),
                Name = model.Name,
                Surname = model.Surname,
                Country = model.Country,
                Region = model.Region,
                City = model.City,
                Street = model.Street,
                PostalCode = model.PostalCode
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }

        public async Task<ApplicationUser> Edit(EditUserDto model, string? name)
        {
            var currentUser = await _userManager.FindByNameAsync(name);
            if (!string.IsNullOrEmpty(model.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(currentUser);
                var res = await _userManager.ResetPasswordAsync(currentUser, token, model.Password);
                if (!res.Succeeded)
                {
                    throw new Exception(res.Errors.First().Description);
                }
            }

            currentUser.PhoneNumber = model.Phone;
            currentUser.Street = model.Street;
            currentUser.PostalCode = model.PostalCode;
            currentUser.Country = model.Country;
            currentUser.Region = model.Region;
            currentUser.City = model.City;
            currentUser.Name = model.Name;
            currentUser.Surname = model.Surname;

            var result = await _userManager.UpdateAsync(currentUser);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            return currentUser;
        }

        public async Task<bool> Delete(string? name)
        {
            var user = await _userManager.FindByNameAsync(name);
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            await _signInManager.SignOutAsync();
            return result.Succeeded;
        }
    }
}
