using CarOperationSystem.DAL.Models;
using CarOperationSystem.DAL.Repository.Interfaces;
using CarOperationSystem.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarOperationSystem.UI.Areas.Admin.Controllers
{

    [Area("admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(Account model)
        {
            IdentityUser identityUser = new IdentityUser() { 
            
              Email = model.Email,
              UserName=model.Email,
            };
            var user = await _userManager.FindByEmailAsync(model.Email);
            await _signInManager.SignInAsync(user, false);
            CookieOptions options = new CookieOptions()
            {
                Domain = "localhost",
                Path = "/",
                Expires = DateTime.Now.AddMinutes(10),
            };

          
                Response.Cookies.Append("email", user.Email, options);
  
                Response.Cookies.Append("fullname", user.Email, options);

                return RedirectToAction("Index", "Home");

                
             
          

            return View(model);
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(Account model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {

                    Email = model.Email,
                    UserName = model.Email,

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else {

                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            else
            {

                return View(model);
            }
            return View();
        }
    }
}
