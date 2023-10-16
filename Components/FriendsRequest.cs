using Clone_Main_Project_0710.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clone_Main_Project_0710.Components
{
    public class FriendsRequest : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<User> listUser = new List<User>();
            return View(listUser);
        }
    }
}