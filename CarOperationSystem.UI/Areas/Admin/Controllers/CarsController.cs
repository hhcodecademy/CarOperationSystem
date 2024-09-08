using CarOperationSystem.DAL.Models;
using CarOperationSystem.DAL.Repository.Interfaces;
using CarOperationSystem.UI.Models;
using CarOperationSystem.UI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace CarOperationSystem.UI.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Operator")]
    public class CarsController : Controller
    {
        private readonly IGenericRepository<Model> _modelRepository;
        private readonly IGenericRepository<Brand> _brandRepository;
        private readonly IGenericRepository<Car> _carRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IFileProvider _fileProvider;

        private readonly IEmailService _emailService;
        public CarsController(IGenericRepository<Model> modelRepository,
            IGenericRepository<Brand> brandRepository,
            IGenericRepository<Car> carRepository,
            IWebHostEnvironment hostEnvironment,
            IFileProvider fileProvider,
            IEmailService emailService)
        {
            _modelRepository = modelRepository;
            _brandRepository = brandRepository;
            _carRepository = carRepository;
            _hostEnvironment = hostEnvironment;
            _fileProvider = fileProvider;
            _emailService = emailService;
        }
        public async Task<IActionResult> Index()
        {
            _emailService.SendMail("AddCars", "aliaea@code.edu.az");
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
                    Thumbnail= car.Thumbnail,
                   
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
                ModelId = model.ModelId,
                Milage = model.Milage,
                Color = model.Color,
            };
            cardModel.Thumbnail = UploadImage(model.Image);
            await _carRepository.Add(cardModel);
    
            return RedirectToAction("Index");
        }
        private string UploadImage(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string localPath = _hostEnvironment.WebRootPath;

            var filePath = Path.Combine(localPath, "images", fileName);

            // Create a new local file and copy contents of the uploaded file
            using (var localFile = System.IO.File.OpenWrite(filePath))
            using (var uploadedFile = file.OpenReadStream())
            {
                uploadedFile.CopyTo(localFile);
            }

            return fileName;
        }
    }
}
