using System.Net.Mime;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.DataSession;
using Clone_Main_Project_0710.GenericMethod;
using Clone_Main_Project_0710.Models.ViewModels;
using Clone_Main_Project_0710.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clone_Main_Project_0710.GendericMethod;
using Clone_Main_Project_0710.Constant;
using BTL.Models.ViewModels;
using Clone_Main_Project_0710.DataCookies;

namespace Clone_Main_Project_0710.Controllers
{
    public class UsersController : Controller
    {
        private UsersRepository _context;
        private UserImagesRepository _imageContext;
        private UserPostsRepository _postContext;
        private UserFriendsRepository _friendContext;
        private Guid userIdTest = new Guid("f98d25ef-75b8-4f1e-8e09-9281d33e8f32");
        private string emailTest = "hoan@gmail.com";
        private void SetSession()
        {
            HttpContext.Session.SetString(DataSession.SessionData.USERID_SESS, userIdTest.ToString());
            HttpContext.Session.SetString(DataSession.SessionData.USER_EMAIL_SESS, emailTest);
        }

        public UsersController(SocialContext context)
        {
            _context = new UsersRepository(context);
            _imageContext = new UserImagesRepository(context);
            _postContext = new UserPostsRepository(context);
            _friendContext = new UserFriendsRepository(context);
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
                if (!HandleCharacter.Instance.IsSpecialChar(user.Password))
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
            string emailSess = Request.Cookies[UsersCookiesConstant.CookieEmail];
            if (emailSess == null)
            {
                return RedirectToAction("Index", "Users");
            }

            Guid userCurrentId = Guid.Parse(Request.Cookies[UsersCookiesConstant.CookieUserId]);

            if(string.IsNullOrEmpty(userId.ToString()))
                return NotFound();

            User user = await _context.FindByID(userId);
            UserImage userImage = await _imageContext.GetAvatarByUserId(userId);
            List<PostView> listPost = await _postContext.GetMyPostsView(userCurrentId, userId);
            List<FriendRequestView> listFriend = await _friendContext.GetFriendsByTargetIdAsync(userId);
            List<UserImage> userImages = await _imageContext.GetImagesByUserId(userId);

            ProfileView profile = new ProfileView()
            {
                User = user,
                ImageAvatar = userImage,
                UserPosts = listPost,
                UserFriends = listFriend,
                UserImages = userImages
            };
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
                errors.Add("Mật khẩu của bạn phải dài ít nhất 6 ký tự. Vui lòng thử một mật khẩu khác.");
                success = false;
            }
            else
            {
                if (!_context.IsExistAccount(email, password))
                {
                    errors.Add("Tài khoản hoặc mật khẩu không đúng");
                    success = false;
                }
            }

            if (success)
            {
                Guid userId = await _context.GetIdByEmail(email, password);
                HttpContext.Session.SetString(SessionData.USERID_SESS, userId.ToString());
                HttpContext.Session.SetString(SessionData.USER_EMAIL_SESS, email);

                SetUserCookie(email, password, userId);
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
        public IActionResult LogoutUser()
        {
            HttpContext.Session.Remove(SessionData.USERID_SESS);
            HttpContext.Session.Remove(SessionData.USER_EMAIL_SESS);

            RemoveUserCookie();
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
            string emailSess = Request.Cookies[UsersCookiesConstant.CookieEmail];
            if (emailSess == null)
            {
                return RedirectToAction("Index", "Users");
            }

            if(userId.Equals(""))
                return NotFound();
            
            ProfileEditView view = new ProfileEditView();

            User user = await _context.FindByID(userId);
            UserImage userImage = await _imageContext.GetAvatarByUserId(userId);

            view.User = user;
            view.UserImage = userImage;

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
                        ImageData = ImageMethod.ConvertImageToString(fileImage),
                        IsAvatar = true,
                        UpdatedAt = DateTime.Now
                    };
                    await _imageContext.UpdateByUserId(userImage);
                }
            }


            return RedirectToAction("ProfileEdit", new { userId = user.UserId });
        }

        

        /// <summary>
        /// Thay đổi password
        /// </summary>
        /// <param name="model">ChangePasswordView</param>
        /// <returns>JsonResult</returns>
        /// Authors: Tạ Đức Hoàn
        /// Create: 20/10/2023
        /// Update: 21/10/2023
        public async Task<JsonResult> ChangePassword(IFormCollection form)
        {
            bool isSuccess = true;
            List<string> errors = new List<string>();
            ChangePasswordView model = new ChangePasswordView()
            {
                UserId = Guid.Parse(form["UserId"].ToString()),
                CurrentPassword = form["CurrentPassword"].ToString(),
                NewPassword = form["NewPassword"].ToString(),
                ConfirmPassword = form["ConfirmPassword"].ToString()
            };

            User user = await _context.FindByID(model.UserId);

            if (model.CurrentPassword.Equals(user.Password))
            {
                if (!model.ConfirmPassword.Equals(model.NewPassword))
                {
                    isSuccess = false;
                    errors.Add("Nhập lại mật khẩu không khớp");
                }
                else
                {
                    if (!HandleCharacter.Instance.IsSixChara(model.NewPassword) ||
                    !HandleCharacter.Instance.IsSpecialChar(model.NewPassword) ||
                    !HandleCharacter.Instance.IsUpperCaseChar(model.NewPassword) ||
                    !HandleCharacter.Instance.IsLowerCaseChar(model.NewPassword) ||
                    !HandleCharacter.Instance.IsDigitChar(model.NewPassword))
                    {
                        isSuccess = false;
                        errors.Add("Mật khẩu chứa ít nhất là 6 ký tự, phải chứa đầy đủ chữ viết thường, chữ viết hoa, số, ký tự đặc biệt.");
                    }
                }
            }
            else
            {
                isSuccess = false;
                errors.Add("Nhập mật khẩu hiện tại không đúng.");
            }

            if (isSuccess)
            {
                await _context.ChangePassword(model);
            }

            return Json(new
            {
                isSuccess = isSuccess,
                errors = errors
            }
            );
        }
        
        public void SetUserCookie(string email, string password, Guid userId)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append(UsersCookiesConstant.CookieEmail, email, options);
            Response.Cookies.Append(UsersCookiesConstant.CookiePassword, password, options);
            Response.Cookies.Append(UsersCookiesConstant.CookieUserId, userId.ToString(), options);
        }

        public User GetUserCookie()
        {
            User user = new User();
            user.Email = Request.Cookies[UsersCookiesConstant.CookieEmail];
            user.Password = Request.Cookies[UsersCookiesConstant.CookiePassword];
            user.UserId = Guid.Parse(Request.Cookies[UsersCookiesConstant.CookieUserId]);

            return user;
        }

        public void RemoveUserCookie()
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Append(UsersCookiesConstant.CookieEmail, "", options);
            Response.Cookies.Append(UsersCookiesConstant.CookiePassword, "", options);
            Response.Cookies.Append(UsersCookiesConstant.CookieUserId, "", options);
        }

        [HttpGet]
        public async Task<IActionResult> TimKiem(string keySearch = null)
        {
            List<PersonView> list = new List<PersonView>();
            if(!string.IsNullOrEmpty(keySearch))
            {
                list = await _context.GetDataByName(keySearch);
            }
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Disable_Enable()
        {
            Guid userCurrentId = Guid.Parse(Request.Cookies[UsersCookiesConstant.CookieUserId]);
            bool disable = await _context.Disable_Enable(userCurrentId);
            return Json(new {
                disable = disable
            });
        }
    }
}