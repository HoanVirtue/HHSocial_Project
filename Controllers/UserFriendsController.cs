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

        [HttpGet]
        public async Task<IActionResult> ConfirmAcceptFriendsRequest(Guid senderId, string type)
        {
            Guid userId = Guid.Parse(HttpContext.Session.GetString(SessionData.USERID_SESS));
            if (type.Equals(TYPE_ACCEPT_CONFIRM)) {
                _context.ConfirmRequestFriend(senderId, userId, TYPE_ACCEPT_CONFIRM);
            }
            else if(type.Equals(TYPE_DELETE_CONFIRM)){
            // xu ly xoa
                _context.ConfirmRequestFriend(senderId, userId, TYPE_DELETE_CONFIRM);
            }
            return View();

            // load lại trang
        }

    }
}