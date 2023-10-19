using System.Net.Mime;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.DataSession;
using Clone_Main_Project_0710.GenericMethod;
using Clone_Main_Project_0710.Models.ViewModels;
using Clone_Main_Project_0710.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clone_Main_Project_0710.Controllers
{
    public class UsersController : Controller
    {
        private UsersRepository _context;
        private UserImagesRepository _imageContext;
        Guid userIdTest = new Guid("fc1a35a7-3564-48cc-b0ce-814e3ff7baab");

        public UsersController(SocialContext context)
        {
            _context = new UsersRepository(context);
            _imageContext = new UserImagesRepository(context);
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
            user.UserId = Guid.NewGuid();
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
            UserImage imageUser = new UserImage();
            imageUser.UserId = user.UserId;
            imageUser.IsAvatar = true;
            imageUser.ImageName = "user_default.png";
            imageUser.ImageData = ImageUserDefault.IMAGE_USER_DEFAULT;

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
                await _imageContext.Add(imageUser);
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
        public async Task<IActionResult> ProfileAsync(Guid userId)
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

            HttpContext.Session.SetString(SessionData.USERID_SESS, userIdTest.ToString());
            HttpContext.Session.SetString(SessionData.USER_EMAIL_SESS, "hoan@gmail.com");
            //end test

            ProfileView profile = new ProfileView();

            User user = await _context.FindByID(userIdTest);
            UserImage userImage = await _imageContext.GetAvatarByUserId(userIdTest);

            profile.user = user;
            profile.imageAvatar = userImage;
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

            if (success)
            {
                Guid userId = await _context.getIdByEmail(email, password);
                HttpContext.Session.SetString(SessionData.USERID_SESS, userId.ToString());
                HttpContext.Session.SetString(SessionData.USER_EMAIL_SESS, email);
            }

            return Json(new
            {
                success = success,
                errors = errors
            });
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns>View đăng nhập</returns>
        /// Authors: Tạ Đức Hoàn
        /// Create: 16/10/2023
        /// Update: 16/10/2023
        public IActionResult logoutUser()
        {
            HttpContext.Session.Remove(SessionData.USERID_SESS);
            HttpContext.Session.Remove(SessionData.USER_EMAIL_SESS);

            return RedirectToAction("Index", "Users");
        }

        /// <summary>
        /// View ProfileEidt
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// Authors: Tạ Đức Hoàn
        /// Create: 16/10/2023
        /// Update: 16/10/2023
        [HttpGet]
        public async Task<IActionResult> ProfileEdit(Guid userId)
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
            HttpContext.Session.SetString(SessionData.USERID_SESS, userIdTest.ToString());
            HttpContext.Session.SetString(SessionData.USER_EMAIL_SESS, "hoan@gmail.com");
            ProfileEditView view = new ProfileEditView();

            User user = await _context.FindByID(userIdTest);
            UserImage userImage = await _imageContext.GetAvatarByUserId(userIdTest);

            view.user = user;
            view.userImage = userImage;

            return View(view);
        }

        /// <summary>
        /// Cập nhập thông tin cá nhân
        /// </summary>
        /// <param name="user"></param>
        /// <returns>View Profile</returns>
        /// Authors: Tạ Đức Hoàn
        /// Create: 16/10/2023
        /// Update: 16/10/2023
        [HttpPost]
        public async Task<IActionResult> ProfileEdit(User user, IFormFile fileImage)
        {
            if (user != null)
            {
                await _context.Update(user);

                if (fileImage != null && fileImage.Length > 0)
                {
                    UserImage userImage = new UserImage
                    {
                        UserId = user.UserId,
                        ImageName = fileImage.FileName,
                        ImageData = ConvertImageToString(fileImage),
                        IsAvatar = true,
                        UpdatedAt = DateTime.Now
                    };
                    await _imageContext.UpdateByUserId(userImage);
                }
            }


            return RedirectToAction("ProfileEdit", new { userId = user.UserId });
        }

        /// <summary>
        /// Chuyển ảnh thành chuỗi, lưu vào database
        /// </summary>
        /// <param name="fileImage">IFormFile</param>
        /// <returns>string</returns>
        /// /// Authors: Tạ Đức Hoàn
        /// Create: 19/10/2023
        /// Update: 19/10/2023
        public string ConvertImageToString(IFormFile fileImage)
        {
            byte[] bytes = null;
            using (Stream fs = fileImage.OpenReadStream())
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    bytes = br.ReadBytes((Int32)fs.Length);
                }
            }
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
    }
}