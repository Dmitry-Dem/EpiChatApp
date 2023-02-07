using EpiChatApp.Data;
using EpiChatApp.Models;
using EpiChatApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpiChatApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            var signInViewModel = new SignInViewModel();
            return View(signInViewModel);
        }
        [HttpGet]
        public IActionResult SignUp()
        {
			var signUpViewModel = new SignUpViewModel();
			return View(signUpViewModel);
        }
        [HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel signInViewModel)
		{
            if (!ModelState.IsValid) return View();

            var user = await _userManager.FindByEmailAsync(signInViewModel.Email);

            if (user != null) 
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, signInViewModel.Password);

                if (checkPassword)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, signInViewModel.Password, false, false);
                    if (result.Succeeded) 
                    {
						return RedirectToAction("Index", "Home");
					}
                }
                TempData["Error"] = "Password is incorrect, Please try again!";
                return View(signInViewModel);
            }
			TempData["Error"] = "User do not found, Please try again!";
			return View(signInViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
		{
			if (!ModelState.IsValid) return View(signUpViewModel);

            var user = await _userManager.FindByEmailAsync(signUpViewModel.Email);

            if (user != null)
            {
                TempData["Error"] = "This addres is already in use";
                return View(signUpViewModel);
            }

            var newUser = new AppUser
            {
                UserName = signUpViewModel.Name,
                Email = signUpViewModel.Email
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, signUpViewModel.Password);

            if (newUserResponse.Succeeded)
            {
				return RedirectToAction("SignIn");
			}

			return RedirectToAction("SignUp");
		}
	}
}

