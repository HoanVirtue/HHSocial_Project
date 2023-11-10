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
        public SearchsRepository(SocialContext context)
        {
            _userContext = new UsersRepository(context);
            _imageContext = new UserImagesRepository(context);
        }
        public async Task<List<TypePersonView>> GetPeopleByKeySearch(Guid userId, string keySearch)
        {
            List<TypePersonView> people = new List<TypePersonView>();
            List<User> listUser = await _userContext.GetUsers_Friends_Avatar_ByKeySearch(keySearch);
            foreach(User u in listUser)
            {
                // Check có phải là bạn bè hay không
                int type = PersonTypeConstant.TYPE_NON_FRIEND;
                UserFriend userFriend = u.SourceUserFriends.Where(m => m.TargetId == userId).FirstOrDefault();
                if(userFriend != null)
                {
                    if(!userFriend.IsFriend)
                        type = PersonTypeConstant.TYPE_REQUEST_FRIEND;
                    else
                        type = PersonTypeConstant.TYPE_FRIEND;
                }
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