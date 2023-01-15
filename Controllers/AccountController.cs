using EpiChatApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EpiChatApp.Models;
using EpiChatApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace EpiChatApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        { 
            _context = context;
            _signInManager= signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            var responce = new SignInViewModel();
            return View(responce);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel)
        {
            if (!ModelState.IsValid) return View(signInViewModel);

            var user = await _userManager.FindByEmailAsync(signInViewModel.Email);

            if (user != null) 
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, signInViewModel.Password);
                
                if (passwordCheck)
                {
                    //Password is correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, signInViewModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                //Password is incorrect, return view
                TempData["Error"] = "Wrong credentails. Please try again";
                return View(signInViewModel);
            }

            //User not found
            TempData["Error"] = "Wrong credentails. Please try again";
            return View(signInViewModel);
        }

		[HttpGet]
		public IActionResult SignUp()
        {
			var responce = new SignUpViewModel();
			return View(responce);
		}

        [HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (!ModelState.IsValid) return View(signUpViewModel);

            var user = await _userManager.FindByEmailAsync(signUpViewModel.Email);

            if (user != null)
            {
                TempData["Error"] = "This email addres is already in use";
                return View(signUpViewModel);
            }

            var newUser = new AppUser()
            {
                UserName = signUpViewModel.Name,
                Email = signUpViewModel.Email
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, signUpViewModel.Password);

            return RedirectToAction("Index", "Home");
        }

    }
}


