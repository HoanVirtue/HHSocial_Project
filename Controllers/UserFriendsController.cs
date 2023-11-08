using BTL.Models.ViewModels;
using Clone_Main_Project_0710.DataSession;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Repository;
using HHSocialNetwork_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;

namespace Clone_Main_Project_0710.Controllers
{
    public class UserFriends : Controller
    {
        public static string TYPE_ACCEPT_CONFIRM = "accept";
        public static string TYPE_DELETE_CONFIRM = "delete";
        private UserFriendsRepository _context;
        public static Guid userIdTest = new Guid("b2b36a90-0354-4f35-bf8a-d35ae7a42011");
        public UserFriends(SocialContext context)
        {
            _context = new UserFriendsRepository(context);
        }

        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString(SessionData.USERID_SESS, userIdTest.ToString());
            HttpContext.Session.SetString(SessionData.USER_EMAIL_SESS, "tungnguyentn1234@gmail.com");

            Guid userId = Guid.Parse(HttpContext.Session.GetString(SessionData.USERID_SESS));
            List<FriendRequestView> listFriend = await _context.GetListFriend(userId);

            return View(listFriend);
        }

        [HttpPost]

        public async Task<JsonResult> ReplyRequestFriend(IFormCollection formData)
        {
            bool isSuccess = true;
            if (formData != null)
            {
                Guid userId = Guid.Parse(HttpContext.Session.GetString(SessionData.USERID_SESS));
                string type = formData["type"].ToString();
                Guid senderId = Guid.Parse(formData["sourceId"].ToString());
                try
                {
                    if (type.Equals(TYPE_ACCEPT_CONFIRM))
                    {
                        await _context.ConfirmRequestFriend(senderId, userId, TYPE_ACCEPT_CONFIRM).ConfigureAwait(false);
                    }
                    else if (type.Equals(TYPE_DELETE_CONFIRM))
                    {
                        await _context.ConfirmRequestFriend(senderId, userId, TYPE_DELETE_CONFIRM);
                    }
                }
                catch (Exception e)
                {
                    isSuccess = false;
                }
            }

            return Json(new {
                successRequest = isSuccess
            });
        }

    }
}