using HHSocialNetwork_Project.DataSession;
using HHSocialNetwork_Project.GenericMethod;
using HHSocialNetwork_Project.Models;
using HHSocialNetwork_Project.Models.ViewModels;
using HHSocialNetwork_Project.Repository;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Đăng ký tài khoản(bất đồng bộ)
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>Json</returns>
        /// Authors: Tạ Đức Hoàn
        /// Create: 17/9/2023
        /// Update: 17/9/2023
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
            
            // create image
            
            // end create

            List<string> errors = new List<string>();
            bool success = true;
            if (_context.ExistByEmail(user.Email))
            {
                errors.Add("Email này đã tồn tại.");
                success = false;
            }
            if (user.Password == null || !user.Password.Equals(rePass))
            {
                errors.Add("Nhập lại mật khẩu không khớp.");
                success = false;
            }
            else
            {
                if (user.Password.Length < 6)
                {
                    errors.Add("Mật khẩu của bạn phải dài ít nhất 6 ký tự. Vui lòng thử một mật khẩu khác.");
                    success = false;
                }
                else
                {
                    if (!HandleCharacter.Instance.IsSpecialChar(user.Password) ||
                    !HandleCharacter.Instance.IsUpperCaseChar(user.Password) ||
                    !HandleCharacter.Instance.IsLowerCaseChar(user.Password) ||
                    !HandleCharacter.Instance.IsDigitChar(user.Password))
                    {
                        errors.Add("Mật khẩu phải chứa đầy đủ chữ viết thường, chữ viết hoa, số, ký tự đặc biệt");
                        success = false;
                    }
                }
            }

            if (success)
            {
                await _context.Add(user);
            }

            return Json(new
            {
                success = success,
                errors = errors
            });
        }

        /// <summary>
        /// Trang thông tin cá nhân: xử lý...
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Json</returns>
        /// Authors: Tạ Đức Hoàn
        /// Create: 17/9/2023
        /// Update: 17/9/2023
        [HttpGet]
        public async Task<IActionResult> ProfileAsync(int userId)
        {
            /*
            string emailSess = HttpContext.Session.GetString(SessionData.USER_EMAIL_SESS);
            if (emailSess == null)
            {
                return RedirectToAction("Index", "Users");
            }

            if(userId == null || userId == 0)
                return NotFound();
            */
            // test
            HttpContext.Session.SetInt32(SessionData.USERID_SESS, 1);
            HttpContext.Session.SetString(SessionData.USER_EMAIL_SESS, "tungnguyentn12345@gmail.com");
            //end test

            ProfileView profile = new ProfileView();

            User user = await _context.FindByID(1);

            profile.user = user;
            return View(profile);
        }
        // <summery>
        // api login body email, password
        // </summery>

        // Authors: Hà
        // Created: 16/9/2023
        // Modified: 17/9/2023
        public async Task<JsonResult> LoginAsync(IFormCollection formCollection)
        {
            var email = formCollection["email"];
            var password = formCollection["password"];

            List<string> errors = new List<string>();
            bool success = true;
            if (Convert.ToString(password).Length < 6)
            {
                errors.Add("Mật khẩu của bạn phải dài ít nhất 8 ký tự. Vui lòng thử một mật khẩu khác.");
                success = false;
            }
            else
            {
                if (!_context.isExistAccount(email, password))
                {
                    errors.Add("Tài khoản hoặc mật khẩu không đúng");
                    success = false;
                }
            }

            if(success) {
                int userId = await _context.getIdByEmail(email, password);
                HttpContext.Session.SetInt32(SessionData.USERID_SESS, userId);
                HttpContext.Session.SetString(SessionData.USER_EMAIL_SESS, email);
            }

            return Json(new
            {
                success = success,
                errors = errors
            });
        }

        public IActionResult logoutUser() {
            HttpContext.Session.Remove(SessionData.USERID_SESS);
            HttpContext.Session.Remove(SessionData.USER_EMAIL_SESS);

            return RedirectToAction("Index", "Users");
        }

        public async Task<IActionResult> ProfileEdit(int userId) {
            /*
            string emailSess = HttpContext.Session.GetString(SessionData.USER_EMAIL_SESS);
            if (emailSess == null)
            {
                return RedirectToAction("Index", "Users");
            }

            if(userId == null || userId == 0)
                return NotFound();
            */
            HttpContext.Session.SetInt32(SessionData.USERID_SESS, 1);
            HttpContext.Session.SetString(SessionData.USER_EMAIL_SESS, "tungnguyentn12345@gmail.com");
            User user = await _context.FindByID(1);

            return View(user);
        }
        
    }
}