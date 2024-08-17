using CarOperationSystem.DAL.Models;
using CarOperationSystem.DAL.Repository.Interfaces;
using CarOperationSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarOperationSystem.UI.ViewComponents
{
    [ViewComponent(Name = "MenuItem")]
    public class SpareItemVC: ViewComponent
    {
        private readonly IGenericRepository<DAL.Models.SpareItem> _repository;

        public SpareItemVC(IGenericRepository<DAL.Models.SpareItem> repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        private async Task<List<SpareItemVM>> GetItemsAsync()
        {
            var itemsTask= await _repository.GetAll();
            var models=itemsTask.ToList();
            var viewModels = new List<SpareItemVM>();
            foreach (var model in models)
            {
                viewModels.Add(new SpareItemVM()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Url = model.Url,

                });
            }
            return viewModels;
        }
    }
}
