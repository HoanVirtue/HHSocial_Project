using Clone_Main_Project_0710.Constant;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using HHSocialNetwork_Project.Models;

namespace Clone_Main_Project_0710.Repository
{
    public class SearchsRepository
    {
        private UsersRepository _userContext;
        private UserImagesRepository _imageContext;
        private UserFriendsRepository _userFriendContext;
        public SearchsRepository(SocialContext context)
        {
            _userContext = new UsersRepository(context);
            _imageContext = new UserImagesRepository(context);
            _userFriendContext = new UserFriendsRepository(context);
        }
        public async Task<List<TypePersonView>> GetPeopleByKeySearch(Guid userId, string keySearch)
        {
            List<TypePersonView> people = new List<TypePersonView>();
            List<User> listUser = await _userContext.GetUsers_Friends_Avatar_ByKeySearch(keySearch);
            foreach(User u in listUser)
            {
                // Check có phải là bạn bè hay không
                int type = -1;
                UserFriend userFriend = u.SourceUserFriends.Where(m => m.TargetId == userId).FirstOrDefault();
                if(userFriend != null)
                {
                    if(!userFriend.IsFriend)
                        type = PersonTypeConstant.TYPE_REPLY_FRIEND;
                    else
                        type = PersonTypeConstant.TYPE_FRIEND;
                }
                else
                {
                    userFriend = await _userFriendContext.GetDataByUser(userId, u.UserId);
                    if(userFriend == null || userFriend.IsDelete == true)
                        type = PersonTypeConstant.TYPE_NON_FRIEND;
                    else
                        type = PersonTypeConstant.TYPE_REQUEST_FRIEND;
                }

                if(u.UserId.Equals(userId))
                    type = PersonTypeConstant.TYPE_IS_YOURSELFT;
                
                TypePersonView person = new TypePersonView()
                {
                    User = u,
                    ImageAvatar = u.UserImages.Where(ui => ui.IsAvatar == true).FirstOrDefault(),
                    TypePerson = type
                };
                people.Add(person);
            }
            return people;
        }
    }
}