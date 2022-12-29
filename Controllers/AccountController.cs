using EpiChatApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EpiChatApp.Models;
using EpiChatApp.Data;

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
        public IActionResult Login()
        {
            var responce = new LoginViewModel();
            return View(responce);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginWiewModel)
        {
            if (!ModelState.IsValid) return View(loginWiewModel);

            var user = await _userManager.FindByEmailAsync(loginWiewModel.Email);

            if (user != null) 
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginWiewModel.Password);
                
                if (passwordCheck)
                {
                    //Password is correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginWiewModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                //Password is incorrect, return view
                TempData["Error"] = "Wrong credentails. Please try again";
                return View(loginWiewModel);
            }

            //User not found
            TempData["Error"] = "Wrong credentails. Please try again";
            return View(loginWiewModel);
        }
    }
}

/*
         public RedirectResult LoginUser(LoginWiewModel loginWiewModel)
        {
            //if (TestDB._users.Exists(person => person.Login == user.Login && person.Password == user.Password))
            //{
            //    return new RedirectResult("/Home/Index", true);
            //}

            //var str = (_users.Exists(person => person.Login == user.Login && person.Password == user.Password ) ? $"{_users.Find(person => person.Login == user.Login && person.Password == user.Password)}\n user is found!" : "user not found!");
            return new RedirectResult("/Home/Index", true);
        }
         */

