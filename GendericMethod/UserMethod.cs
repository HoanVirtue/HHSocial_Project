using Clone_Main_Project_0710.Constant;
using Clone_Main_Project_0710.DataCookies;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Repository;

namespace Clone_Main_Project_0710.GendericMethod
{
    public class UserMethod
    {
        private static UserImagesRepository _imageContext;
        private static UsersRepository _userContext;
        public UserMethod(SocialContext context)
        {
            _imageContext = new UserImagesRepository(context);
            _userContext = new UsersRepository(context);
        }
        public static async Task<User> ConvertMessToUser(UserMessage userMessage)
        {
            Guid userCurrentId = UsersCookies.GetUserCookie().UserId;
            User friend;
            if(userMessage.SourceId.Equals(userCurrentId))
                friend = await _userContext.FindByID(userMessage.TargetId);
            else
                friend = await _userContext.FindByID(userMessage.SourceId);
            return friend;
        }
    }
}