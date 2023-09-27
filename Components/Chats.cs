using Microsoft.AspNetCore.Mvc;

namespace HHSocialNetwork_Project.Components
{
    public class Chats : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}