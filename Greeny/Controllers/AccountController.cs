using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.Account;

namespace Greeny.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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

            AppUser user = await _userManager.FindByEmailAsync(request.EmailOrUsername);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(request.EmailOrUsername);
            }

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");
                return View(request);
            }

            PasswordVerificationResult comparePassword = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (comparePassword.ToString() == "Failed")
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");
                return View(request);
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

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

            AppUser user = new()
            {
                UserName = request.Username,
                Fullname = request.Fullname,
                Email = request.Email,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(request);
            }

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token }, Request.Scheme);

            string html = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/account.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{link}}", link);

            html = html.Replace("{{fullName}}", user.Fullname);

            string subject = "Email confirmation";

            _emailService.SendEmail(request.Email, subject, html);

            return RedirectToAction(nameof(VerifyEmail));
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();

            AppUser user = await _userManager.FindByIdAsync(userId);

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            ViewBag.fullName = user.Fullname;
            ViewBag.email = user.Email;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
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

            AppUser user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                ModelState.AddModelError("Email", "User with this email does not exist");
                return View();
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = user.Id, token }, Request.Scheme);

            string html = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/account.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{link}}", link);

            html = html.Replace("{{fullName}}", user.Fullname);

            string subject = "Reset password";

            _emailService.SendEmail(request.Email, subject, html);

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

            AppUser user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null) return NotFound();

            if (await _userManager.CheckPasswordAsync(user, request.Password))
            {
                ModelState.AddModelError("RepeatPassword", "You cannot use your current password");
                return View(request);
            }

            await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            return RedirectToAction(nameof(Login));
        }
    }
}
