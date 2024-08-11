using CarOperationSystem.DAL.Repository.Interfaces;
using CarOperationSystem.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarOperationSystem.UI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
      
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(Account model)
        {
            if (model.Email=="admin@gmail.com"&&model.Password=="admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

    }
}
