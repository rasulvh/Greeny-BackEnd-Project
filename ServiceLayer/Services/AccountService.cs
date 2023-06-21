using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Ocsp;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.Account;

namespace ServiceLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IBasketRepository _basketRepository;

        public AccountService(UserManager<AppUser> userManager,
                              IEmailService emailService,
                              SignInManager<AppUser> signInManager,
                              RoleManager<IdentityRole> roleManager,
                              IBasketRepository basketRepository)
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _basketRepository = basketRepository;
        }

        public async Task AddRoleToUserAsync(AppUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public Task<bool> CheckRoleExistAsync(string role)
        {
            return _roleManager.RoleExistsAsync(role);
        }

        public PasswordVerificationResult ComparePassword(AppUser user, string password)
        {
            return _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        }

        public async Task ConfirmEmail(string userId, string token)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);
        }

        public async Task CreateRoleAsync(string role)
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = role });
        }

        public async Task<IdentityResult> CreateUser(RegisterVM request)
        {
            AppUser user = new()
            {
                UserName = request.Username,
                Fullname = request.Fullname,
                Email = request.Email,
            };

            return await _userManager.CreateAsync(user, request.Password);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameOrEmail(string emailOrUsername)
        {
            AppUser user = await _userManager.FindByEmailAsync(emailOrUsername);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(emailOrUsername);
            }

            return user;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task ResetPasswordAsync(AppUser user, string token, string password)
        {
            await _userManager.ResetPasswordAsync(user, token, password);
        }

        public void SendConfirmationEmail(AppUser user, string email, string subject, string link)
        {
            string html = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/account.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{link}}", link);

            html = html.Replace("{{fullName}}", user.Fullname);

            _emailService.SendEmail(email, subject, html);
        }

        public async Task<SignInResult> SignInAsync(AppUser user, string password)
        {
            return await _signInManager.PasswordSignInAsync(user, password, false, false);
        }
    }
}
