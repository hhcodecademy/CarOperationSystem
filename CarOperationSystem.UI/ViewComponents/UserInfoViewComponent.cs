using CarOperationSystem.DAL.Models;
using CarOperationSystem.DAL.Repository.Interfaces;
using CarOperationSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace CarOperationSystem.UI.ViewComponents
{
    public class UserInfoViewComponent : ViewComponent
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserInfoViewComponent(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await GetUserInfo();
            return View(user);
        }

        private async Task<User> GetUserInfo()
        {

            string email = Request.Cookies["email"];
            var users = await _userRepository.GetAll();
            User user = null;
            foreach (var item in users)
            {
                if (item.Email == email)
                {
                    user= item;
                }
            }
            return user;
        }
    }
}
