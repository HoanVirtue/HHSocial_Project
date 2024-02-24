using BTL.Models.ViewModels;
using Clone_Main_Project_0710.Constant;
using Clone_Main_Project_0710.DataCookies;
using Clone_Main_Project_0710.DataSession;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Repository;
using HHSocialNetwork_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HHSocialNetwork_Project.Components
{
    public class FriendsRequest : ViewComponent
    {
        public static string GET_ALL = "all";
        public static string GET_DROPDOWN = "3";
        private UserFriendsRepository _context;
         public FriendsRequest(SocialContext context)
        {
            _context = new UserFriendsRepository(context);
        }
        public async Task< IViewComponentResult> InvokeAsync()
        {
            Guid userId = UsersCookies.GetUserCookie().UserId;
            List<FriendRequestView> listFriend = await _context.GetListFriend(userId);
            
            return View(listFriend);
        }

    }
}