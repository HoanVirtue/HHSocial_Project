using Microsoft.AspNetCore.Mvc;

namespace HHSocialNetwork_Project.Components
{
    public class FriendsRequest : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}