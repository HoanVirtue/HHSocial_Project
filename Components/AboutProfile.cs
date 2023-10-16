using Clone_Main_Project_0710.DataSession;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using Clone_Main_Project_0710.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Clone_Main_Project_0710.Components
{
    public class AboutProfile : ViewComponent
    {
        private UsersRepository _context;
        public AboutProfile(SocialContext context)
        {
            _context = new UsersRepository(context);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Guid userId = Guid.Parse(HttpContext.Session.GetString(SessionData.USERID_SESS));
            AboutProfileView aboutView = new AboutProfileView();

            User user = await _context.FindByID(userId);
            
            aboutView.user = user;
            return View(aboutView);
        }
    }
}