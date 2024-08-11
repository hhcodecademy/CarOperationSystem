using CarOperationSystem.DAL.Models;
using CarOperationSystem.DAL.Repository.Interfaces;
using CarOperationSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarOperationSystem.UI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CarsController : Controller
    {
        private readonly IGenericRepository<Model> _modelRepository;
        private readonly IGenericRepository<Brand> _brandRepository;
        private readonly IGenericRepository<Car> _carRepository;
        public CarsController(IGenericRepository<Model> modelRepository,
            IGenericRepository<Brand> brandRepository,
            IGenericRepository<Car> carRepository)
        {
            _modelRepository = modelRepository;
            _brandRepository = brandRepository;
            _carRepository = carRepository;
        }
        public async Task<IActionResult> Index()
        {
            var carTasks = await _carRepository.GetAll();
            List<CarVM> carModels = new List<CarVM>();
            var cars = carTasks.ToList();
            foreach (var car in cars)
            {
                carModels.Add(new CarVM
                {
                    Id = car.Id,
                    BrandId = car.BrandId,
                    BrandName = _brandRepository.Get(car.BrandId).Result.Name,
                    ModelId = car.ModelId,
                    ModelName = _modelRepository.Get(car.ModelId).Result.Name,
                    Milage = car.Milage,
                    Color = car.Color,
                });
            }
            return View(carModels);
        }
        public async Task<IActionResult> Create(int? brandId, int? modelId)
        {
            CarVM modelView = new CarVM();
            var brands = await _brandRepository.GetAll();
            modelView.Brands = brands.ToList();
            modelView.BrandId = brandId ?? 0; 
            var models = await _modelRepository.GetAll();
            modelView.Models = models.ToList();
            modelView.ModelId = modelId ?? 0;
            return View(modelView);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CarVM model)
        {
            Car cardModel = new Car()
            {
                BrandId = model.BrandId,
                ModelId= model.ModelId,
              Milage= model.Milage,
              Color = model.Color,
            };
            await _carRepository.Add(cardModel);
            return RedirectToAction("Index");
        }
    }
}
