using CarOperationSystem.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarOperationSystem.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = new List<UserVM>();

            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                model.Add(new UserVM
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = roles
                });
            }

            return View(model);
        }
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            var userVM = new UserVM
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Roles = roles
            };

            return View(userVM);
        }


        public async Task<IActionResult> GetPartialData(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            var userVM = new UserVM
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Roles = roles
            };

            return PartialView("_UserRole", userVM);
        }

        public async Task<IActionResult> AssignRole(string userId)
        {
            var model = new UserRoleVM()
            {
                Roles = new List<RoleVM>()
            };

            var user = await _userManager.FindByIdAsync(userId);

            var roles = _roleManager.Roles.ToList();

            model.UserName = user.UserName;
            model.UserId = user.Id;


            foreach (var role in roles)
            {
                model.Roles.Add(new RoleVM
                {
                    Id = role.Id,
                    Name = role.Name
                });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(UserRoleVM userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId);
            var role = await _roleManager.FindByIdAsync(userRole.RoleId);

            var userRoles = await _userManager.GetRolesAsync(user);

            if (!userRoles.Contains(role.Name))
            {
                await _userManager.AddToRoleAsync(user, role.Name);
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
