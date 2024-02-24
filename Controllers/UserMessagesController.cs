using Clone_Main_Project_0710.DataCookies;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using Clone_Main_Project_0710.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Clone_Main_Project_0710.Controllers
{
    public class UserMessagesController : Controller
    {
        private UserMessagesRepository _messContext;
        public UserMessagesController(SocialContext context)
        {
            _messContext = new UserMessagesRepository(context);
        }

        [HttpPost]
        public async Task<IActionResult> SearchUserInChats(string keySearch)
        {
            bool isSuccess = true;
            string errorMsg = "";
            List<UserMessage_FriendView> listUser = null;
            try
            {
                Guid userCurrentId = UsersCookies.GetUserCookie().UserId;
                listUser = await _messContext.GetAllFilterFriend(userCurrentId, keySearch);
            }
            catch(Exception ex)
            {
                isSuccess = false;
                errorMsg = ex.Message.ToString();
            }
            

            return Json(new {
                isSuccess = isSuccess,
                errorMsg = errorMsg,
                listUser = listUser
            });
        }
    }
}