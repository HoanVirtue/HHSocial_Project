using Clone_Main_Project_0710.Models;
using HHSocialNetwork_Project.DataSession;
using HHSocialNetwork_Project.Models;
using HHSocialNetwork_Project.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HHSocialNetwork_Project.Components
{
    public class FriendsRequest : ViewComponent
    {  
        private UserFriendsRepository _context;
         public FriendsRequest(SocialContext context)
        {
            _context = new UserFriendsRepository(context);
        }
        public async Task< IViewComponentResult> InvokeAsync()
        {
            List<UserFriend> listUserFiends = await _context.GetAll();
            return View(listUserFiends);
        }
    }
}