using Clone_Main_Project_0710.DataSession;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using Clone_Main_Project_0710.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Clone_Main_Project_0710.Components
{
    public class Sidebar : ViewComponent
    {
        private UsersRepository _context;
        public Sidebar(SocialContext context)
        {
            _context = new UsersRepository(context);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Guid userId = Guid.Parse(HttpContext.Session.GetString(SessionData.USERID_SESS));
            SidebarView sidebar = new SidebarView();
            User user = await _context.FindByID(userId);
            
            sidebar.User = user;
            return View(sidebar);
        }
    }
}