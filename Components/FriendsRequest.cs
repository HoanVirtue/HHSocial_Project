using BTL.Models.ViewModels;
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
        private UserImagesRepository _imageContext;
         public FriendsRequest(SocialContext context)
        {
            _context = new UserFriendsRepository(context);
            _imageContext = new UserImagesRepository(context);
        }
        public async Task< IViewComponentResult> InvokeAsync()
        {
            List<FriendRequestView> listFriend = await GetListFriend();
            
            return View(listFriend);
        }

        private async Task<List<FriendRequestView>> GetListFriend()
        {
            List<FriendRequestView> listFriend = new List<FriendRequestView>();

            List<UserFriend> userFriends = await _context.GetAll();
            foreach(UserFriend u in userFriends)
            {
                UserImage image = await _imageContext.GetAvatarByUserId(u.SourceId);

                FriendRequestView friend = new FriendRequestView()
                {
                    UserFriend = u,
                    UserImage = image
                };

                listFriend.Add(friend);
            }

            return listFriend;
        }

    }
}