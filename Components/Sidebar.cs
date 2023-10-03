using HHSocialNetwork_Project.DataSession;
using HHSocialNetwork_Project.Models;
using HHSocialNetwork_Project.Models.ViewModels;
using HHSocialNetwork_Project.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HHSocialNetwork_Project.Components
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
            int userId = (int)HttpContext.Session.GetInt32(SessionData.USERID_SESS);
            SidebarView sidebar = new SidebarView();
            User user = await _context.FindByID(userId);
            
            sidebar.user = user;
            return View(sidebar);
        }
    }
}