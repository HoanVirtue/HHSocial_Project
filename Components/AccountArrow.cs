using HHSocialNetwork_Project.Models;
using HHSocialNetwork_Project.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HHSocialNetwork_Project.Components
{
    public class AccountArrow : ViewComponent
    {
        private IRepository<User> _context;
        public AccountArrow(IRepository<User> context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            User user = await _context.FindByID(1);
            return View(user);
        }
    }
}