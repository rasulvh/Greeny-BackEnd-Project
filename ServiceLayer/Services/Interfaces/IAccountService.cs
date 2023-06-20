using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.ViewModels.Account;

namespace ServiceLayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateUser(RegisterVM request);
        Task<AppUser> GetUserByUsernameOrEmail(string emailOrUsername);
        Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
        void SendConfirmationEmail(AppUser user, string email, string subject, string link);
        PasswordVerificationResult ComparePassword(AppUser user, string password);
        Task<SignInResult> SignInAsync(AppUser user, string password);
        Task ConfirmEmail(string userId, string token);
        Task<AppUser> GetUserById(string id);
        Task LogoutAsync();
        Task<string> GeneratePasswordResetTokenAsync(AppUser user);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task ResetPasswordAsync(AppUser user, string token, string password);
        Task<bool> CheckRoleExistAsync(string role);
        Task CreateRoleAsync(string role);
        Task AddRoleToUserAsync(AppUser user, string role);
    }
}
