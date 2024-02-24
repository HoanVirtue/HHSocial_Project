using Clone_Main_Project_0710.DataCookies;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using Clone_Main_Project_0710.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Clone_Main_Project_0710.Components
{
    public class Chats : ViewComponent
    {
        public UserMessagesRepository _messContext;
        public Chats(SocialContext context)
        {
            _messContext = new UserMessagesRepository(context);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Guid userId = UsersCookies.GetUserCookie().UserId;
            List<UserMessage_FriendView> listMess = await _messContext.GetAllFilterFriend(userId, null);
            
            return View(listMess);
        }
    }
}