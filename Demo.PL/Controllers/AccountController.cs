using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.Models.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = registerViewModel.Email.Split('@')[0],
                    Email = registerViewModel.Email,
                    IsAgree = registerViewModel.IsAgree,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                };
                var result = await userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }
            }
            return View(registerViewModel);

        }

        public IActionResult Login() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginViewModel.Email);
                if (user is not null)
                {
                    var result = await userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (result)
                    {
                        var loginResult = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                        if (loginResult.Succeeded)
                        {
                            return RedirectToAction("index", "Home");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Password is incorrect!");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is not found!");
                }
            }
            return View(loginViewModel);

        }


        [HttpGet]

        public IActionResult ForgotYourPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotYourPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {

                    var code = await userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordLink = Url.Action("ResetYourPassword", "Account", new { email = model.Email, token = code }, Request.Scheme);
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordLink

                    };
                    EmailSettings.SendEmail(email);

                    // Generate password reset token


                    // Send password reset email
                    // ...

                    return RedirectToAction("CheckYourInbox");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email doesn't exist!");
                }
                // Handle email not found error
            }
            return View(model);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }


        public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult ResetYourPassword(string Email,string Token)
        {
            if(Email is null || Token is null)
            {
                return BadRequest();
            }
            TempData["Email"] = Email;
            TempData["token"] = Token;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> ResetYourPassword(ResetPasswordViewModel model)
        {
            model.Email = TempData["Email"]?.ToString()??string.Empty;
            model.Token = TempData["Token"]?.ToString() ?? string.Empty;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var user = userManager.FindByEmailAsync(model.Email).Result;
                if(user is not null)
                {
                    var result = userManager.ResetPasswordAsync(user, model.Token, model.Password).Result;
                    if (result.Succeeded) {
                        return RedirectToAction(nameof(Login));
                    }
                    else
                    {
                        foreach (var errors in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, errors.Description);
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "User is not found!");
                return View(model);
            }
        }
    }
}
