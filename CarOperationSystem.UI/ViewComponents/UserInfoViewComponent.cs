using CarOperationSystem.DAL.Models;
using CarOperationSystem.DAL.Repository.Interfaces;
using CarOperationSystem.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace CarOperationSystem.UI.ViewComponents
{
    public class UserInfoViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserInfoViewComponent( UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await GetUserInfo();
            return View(user);
        }

        private async Task<IdentityUser> GetUserInfo()
        {

            string username = User.Identity.Name; ;
            var user = await _userManager.FindByNameAsync(username);
         
            return user;
        }
    }
}
