using HHSocialNetwork_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace HHSocialNetwork_Project.Components
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