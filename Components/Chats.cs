using Microsoft.AspNetCore.Mvc;

namespace Clone_Main_Project_0710.Components
{
    public class Chats : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}