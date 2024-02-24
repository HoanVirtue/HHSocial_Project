using Clone_Main_Project_0710.Constant;
using Clone_Main_Project_0710.Models;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Clone_Main_Project_0710.DataCookies
{
    public class UsersCookies
    {
        public static string EMAIL_COOKIE = "email_cookie";
        public static string PASSWORD_COOKIE = "password_cookie";
        public static string USERID_COOKIE = "userid_cookie";
        private static HttpContext _httpContext => new HttpContextAccessor().HttpContext;
        
        public static void SetUserCookie(string email, string password, Guid userId)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            _httpContext.Response.Cookies.Append(EMAIL_COOKIE, email, options);
            _httpContext.Response.Cookies.Append(PASSWORD_COOKIE, password, options);
            _httpContext.Response.Cookies.Append(USERID_COOKIE, userId.ToString(), options);
        }

        public static User GetUserCookie()
        {
            User user = new User();
            user.Email = _httpContext.Request.Cookies[EMAIL_COOKIE];
            user.Password = _httpContext.Request.Cookies[PASSWORD_COOKIE];
            string userId = _httpContext.Request.Cookies[USERID_COOKIE];
            user.UserId = string.IsNullOrEmpty(userId) ? Guid.Empty : Guid.Parse(userId);

            return user;
        }

        public static void RemoveUserCookie()
        {
            _httpContext.Response.Cookies.Delete(EMAIL_COOKIE);
            _httpContext.Response.Cookies.Delete(PASSWORD_COOKIE);
            _httpContext.Response.Cookies.Delete(USERID_COOKIE);
        }
    }
}