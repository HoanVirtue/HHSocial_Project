using Clone_Main_Project_0710.Constant;
using Clone_Main_Project_0710.DataCookies;
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
            Guid userId = UsersCookies.GetUserCookie().UserId;
            AboutProfileView aboutView = new AboutProfileView();

            User user = await _context.FindByID(userId);
            
            aboutView.User = user;
            return View(aboutView);
        }
    }
}