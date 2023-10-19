using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Repository;
using HHSocialNetwork_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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