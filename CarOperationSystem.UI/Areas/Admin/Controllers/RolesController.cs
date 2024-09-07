using CarOperationSystem.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarOperationSystem.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var model = new List<RoleVM>();

            var roles = _roleManager.Roles.ToList();

            foreach (var role in roles)
            {
                model.Add(new RoleVM
                {
                    Id = role.Id, 
                    Name = role.Name
                });
            }


            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleVM role)
        {
            if (ModelState.IsValid)
            {
                var roleToCreate = new IdentityRole
                {
                    Name = role.Name,
                };

                await _roleManager.CreateAsync(roleToCreate);

                return RedirectToAction("Index");
            }

            return View(role);
        }


        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var model = new RoleVM
            {
                Id = id,
                Name = role.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleVM role)
        {
            if (ModelState.IsValid)
            {
                var roleToUpdate = await _roleManager.FindByIdAsync(role.Id);

                roleToUpdate.Name = role.Name;

                await _roleManager.UpdateAsync(roleToUpdate);

                return RedirectToAction("Index");
            }

            return View(role);
        }


    }
}
