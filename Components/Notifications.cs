using Microsoft.AspNetCore.Mvc;

namespace HHSocialNetwork_Project.Components
{
    public class Notifications : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}