using CarOperationSystem.DAL.Models;
using CarOperationSystem.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarOperationSystem.UI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class BrandsController : Controller
    {
        private readonly IGenericRepository<Brand> _brandRepository;

        public BrandsController(IGenericRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }


        public async Task<IActionResult> Index()
        {
            var brands = await _brandRepository.GetAll();
            return View(brands.ToList());
        }


        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Brand model)
        {
            await _brandRepository.Add(model);
            return RedirectToAction("Index");
        }
    }

}
