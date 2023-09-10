using HHSocialNetwork_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace HHSocialNetwork_Project.Controllers
{
    public class UsersController : Controller
    {
        private SocialContext _context;

        public UsersController(SocialContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}