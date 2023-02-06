using EpiChatApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EpiChatApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        /*[HttpPost]
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
        }*/

    }
}

