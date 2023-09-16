using System.Xml;
using HHSocialNetwork_Project.GenericMethod;
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
            user.UserName = user.FirstName + " " + user.LastName;
            user.Avatar = "/user/user_default.png";
            user.CoverImage = "/page-img/profile-bg1.jpg";
            
            List<string> errors = new List<string>();
            bool success = true;
            if(_context.ExistByEmail(user.Email))
            {
                errors.Add("Email này đã tồn tại.");
                success = false;
            }
            if(user.Password == null || !user.Password.Equals(rePass))
            {
                errors.Add("Nhập lại mật khẩu không khớp.");
                success = false;
            }
            else
            {
                if(user.Password.Length < 6) {
                    errors.Add("Mật khẩu của bạn phải dài ít nhất 6 ký tự. Vui lòng thử một mật khẩu khác.");
                    success = false;
                } else {
                    if(!HandleCharacter.Instance.IsSpecialChar(user.Password) || 
                    !HandleCharacter.Instance.IsUpperCaseChar(user.Password) || 
                    !HandleCharacter.Instance.IsLowerCaseChar(user.Password) || 
                    !HandleCharacter.Instance.IsDigitChar(user.Password)) {
                        errors.Add("Mật khẩu phải chứa đầy đủ chữ viết thường, chữ viết hoa, số, ký tự đặc biệt");
                        success = false;
                    }
                }
            }

            if(success) {
                await _context.Add(user);
            }

            return Json(new {
                success = success,
                errors = errors
            });
        }

        [HttpGet]
        public IActionResult Profile(int userId)
        {
            
            return View();
        }

    }

    
}