using CarOperationSystem.DAL.Models;
using CarOperationSystem.DAL.Repository.Interfaces;
using CarOperationSystem.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarOperationSystem.UI.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "ReportManager")]
    public class ModelsController : Controller
    {
        private readonly IGenericRepository<Model> _modelRepository;
        private readonly IGenericRepository<Brand> _brandRepository;
        public ModelsController(IGenericRepository<Model> modelRepository, IGenericRepository<Brand> brandRepository)
        {
            _modelRepository = modelRepository;
            _brandRepository = brandRepository;
        }

        public async Task<IActionResult> Index()
        {
            var modelTasks = await _modelRepository.GetAll();
            List<ModelVM> carModels = new List<ModelVM>();
            var models = modelTasks.ToList();
            foreach (var model in models)
            {
                carModels.Add(new ModelVM
                {
                    Id = model.Id,
                    Name = model.Name,
                    BrandId = model.BrandId,
                    BrandName = _brandRepository.Get(model.BrandId).Result.Name
                });
            }
            return View(carModels);
        }

        public async Task<IActionResult> Create(int? brandId)
        {
            ModelVM modelView = new ModelVM();
            var brands = await _brandRepository.GetAll();
            modelView.Brands = brands.ToList();
            modelView.BrandId = brandId ?? 0;
            return View(modelView);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ModelVM model)
        {
            Model cardModel = new Model()
            {
                BrandId = model.BrandId,
                Name = model.Name,
            };
            await _modelRepository.Add(cardModel);
            return RedirectToAction("Index");
        }
    }
}
