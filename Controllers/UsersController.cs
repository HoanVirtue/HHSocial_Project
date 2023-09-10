using System.Xml;
using HHSocialNetwork_Project.Models;
using HHSocialNetwork_Project.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HHSocialNetwork_Project.Controllers
{
    public class UsersController : Controller
    {
        private UsersRepository _context;

        public UsersController(SocialContext context)
        {
            _context = new UsersRepository(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RegisterAsync(IFormCollection formCollection)
        {
            User user = new User();
            user.FirstName = formCollection["FirstName"];
            user.LastName = formCollection["LastName"];
            user.Email = formCollection["Email"];
            user.Password = formCollection["Password"];
            string? rePass = formCollection["RetyePassword"];
            user.Birthday = formCollection["Birthday"].ToString().Equals("") ? null : DateTime.Parse(formCollection["Birthday"].ToString());
            user.GenderName = formCollection["GenderName"];
            user.RegisterAt = DateTime.Now;
            
            List<string> errors = new List<string>();
            bool success = true;
            if(user.Password == null || !user.Password.Equals(rePass)) {
                errors.Add("Nhập lại mật khẩu không khớp");
                success = false;
            }
            
            if(_context.ExistByEmail(user.Email))
            {
                errors.Add("Email này đã tồn tại");
                success = false;
            }

            if(success) {
                await _context.Add(user);
            }

            return Json(new {
                success = success,
                errors = errors
            });
        }
    }
}