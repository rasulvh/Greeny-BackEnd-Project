using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.Account;
using ServiceLayer.Helpers;

namespace Greeny.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            AppUser user = await _accountService.GetUserByUsernameOrEmail(request.EmailOrUsername);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");
                return View(request);
            }

            PasswordVerificationResult comparePassword = _accountService.ComparePassword(user, request.Password);

            if (comparePassword.ToString() == "Failed")
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");
                return View(request);
            }

            var result = await _accountService.SignInAsync(user, request.Password);

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Please confirm your account");
                return View(request);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _accountService.CreateUser(request);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(request);
            }

            AppUser user = await _accountService.GetUserByUsernameOrEmail(request.Username);

            await _accountService.AddRoleToUserAsync(user, Roles.Member.ToString());

            string token = await _accountService.GenerateEmailConfirmationTokenAsync(user);

            string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token }, Request.Scheme);

            string subject = "Email confirmation";

            _accountService.SendConfirmationEmail(user, request.Email, subject, link);

            return RedirectToAction(nameof(VerifyEmail));
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();

            await _accountService.ConfirmEmail(userId, token);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _accountService.GetUserById(userId);

            if (user == null) return NotFound();

            ProfileVM model = new()
            {
                Fullname = user.Fullname,
                Email = user.Email
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = await _accountService.GetUserByUsernameOrEmail(request.Email);

            if (user is null)
            {
                ModelState.AddModelError("Email", "User with this email does not exist");
                return View();
            }

            string token = await _accountService.GeneratePasswordResetTokenAsync(user);

            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = user.Id, token }, Request.Scheme);

            string subject = "Reset password";

            _accountService.SendConfirmationEmail(user, request.Email, subject, link);

            return RedirectToAction(nameof(VerifyEmail));
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            ResetPasswordVM model = new()
            {
                UserId = userId,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            AppUser user = await _accountService.GetUserById(request.UserId);

            if (user is null) return NotFound();

            if (await _accountService.CheckPasswordAsync(user, request.Password))
            {
                ModelState.AddModelError("RepeatPassword", "You cannot use your current password");
                return View(request);
            }

            await _accountService.ResetPasswordAsync(user, request.Token, request.Password);

            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _accountService.CheckRoleExistAsync(role.ToString()))
                {
                    await _accountService.CreateRoleAsync(role.ToString());
                }
            }

            return Ok();
        }
    }
}
