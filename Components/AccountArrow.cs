using Clone_Main_Project_0710.DataSession;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Clone_Main_Project_0710.Components
{
    public class AccountArrow : ViewComponent
    {
        private UsersRepository _context;
        public AccountArrow(SocialContext context)
        {
            _context = new UsersRepository(context);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Guid userId = Guid.Parse(HttpContext.Session.GetString(SessionData.USERID_SESS));
            User user = await _context.FindByID(userId);
            
            return View(user);
        }
    }
}