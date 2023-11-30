using Clone_Main_Project_0710.Constant;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Clone_Main_Project_0710.DataCookies
{
    public class UsersCookies
    {
        // public static Guid GetUserIdCurrent()
        // {

        // }

        public static void SetUserIdCookie(string email, string password, Guid userId)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            // Response.Cookies.Append(UsersCookiesConstant.CookieUserId, userId, options);
            
        }
    }
}