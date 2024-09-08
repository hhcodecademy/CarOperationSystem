using CarOperationSystem.DAL.Models;
using CarOperationSystem.DAL.Repository.Interfaces;
using CarOperationSystem.UI.Areas.Admin.Models;
using CarOperationSystem.UI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace CarOperationSystem.UI.Areas.Admin.Controllers
{

    [Area("admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailService _emailService;
        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(AccountVM model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.Persistent, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();




        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> ChangePassword(string userId,  string token, AccountVM model)
        {
            var user = await  _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return View();
            }
            else
            {
               var result= await  _userManager.ResetPasswordAsync(user, token, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("LogIn", "Account");
                }
            
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(AccountVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Email not found";
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);



            string createdUrl = Url.Action("ChangePassword", "Account", new { userId = user.Id, Token = token },HttpContext.Request.Scheme);

            _emailService.SendMail(createdUrl, "huseyn.hasanli@code.edu.az");

            ViewBag.SuccessMessage = "Email sent to mail";

            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(AccountVM model)
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
                else
                {

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
