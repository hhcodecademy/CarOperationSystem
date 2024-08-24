using CarOperationSystem.DAL.Models;
using CarOperationSystem.DAL.Repository.Interfaces;
using CarOperationSystem.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarOperationSystem.UI.Areas.Admin.Controllers
{

    [Area("admin")]
    public class AccountController : Controller
    {
        private readonly IGenericRepository<User> _userRepository;
        public AccountController(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(Account model)
        {
            var users = await _userRepository.GetAll();
            CookieOptions options = new CookieOptions()
            {
                Domain = "localhost",
                Path = "/",
                Expires = DateTime.Now.AddMinutes(10),
            };
            foreach (var user in users)
            {
                if (user.Email == model.Email && user.Password == model.Password)
                {

                    Response.Cookies.Append("email", user.Email, options);
                    Response.Cookies.Append("fullname", user.Name+" "+user.Surname, options);

                    return RedirectToAction("Index", "Home");
                }
            }
        
            return View(model);
        }

    }
}
