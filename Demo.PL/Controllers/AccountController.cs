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
				var code = await userManager.GeneratePasswordResetTokenAsync(user);
				if (user is not null)
				{

					var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = user.Email , token = code}, "https", "localhost:44377");
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
	}
}
