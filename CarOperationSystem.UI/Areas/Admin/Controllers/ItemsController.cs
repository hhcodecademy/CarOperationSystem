using CarOperationSystem.DAL.Models;
using CarOperationSystem.DAL.Repository.Interfaces;
using CarOperationSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarOperationSystem.UI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ItemsController : Controller
    {
        private readonly IGenericRepository<SpareItem> _itemRepository;

        public ItemsController(IGenericRepository<SpareItem> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IActionResult> Index()
        {
            var itemTask = await _itemRepository.GetAll();

            var items = itemTask.ToList();

            var models = new List<SpareItemVM>();

            foreach (var item in items)
            {
                models.Add(new SpareItemVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Url = item.Url,
                });
            }

            return View(models);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpareItemVM model)
        {
            var spareItem = new SpareItem
            {
                Name = model.Name,
                Description = model.Description,
                Url = model.Url,
                CreateDate = DateTime.Now
            };

            await _itemRepository.Add(spareItem);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var item = await _itemRepository.Get(Id);
            var model = new SpareItemVM
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Url = item.Url
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SpareItemVM model)
        {
            _itemRepository.Remove(model.Id);

            return RedirectToAction("Index");
        }

    }
}
