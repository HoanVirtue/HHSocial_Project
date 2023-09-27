using HHSocialNetwork_Project.Models;
using HHSocialNetwork_Project.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HHSocialNetwork_Project.Components
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
            User user = await _context.FindByID(1);
            return View(user);
        }
    }
}