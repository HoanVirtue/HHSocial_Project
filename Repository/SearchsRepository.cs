using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;

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
        public async Task<List<ProfileView>> GetPeopleByKeySearch(string keySearch)
        {
            List<ProfileView> people = new List<ProfileView>();
            List<User> listUser = await _userContext.GetUsers_Friends_Avatar_ByKeySearch(keySearch);
            foreach(User u in listUser)
            {
                ProfileView person = new ProfileView()
                {
                    User = u,
                    ImageAvatar = u.UserImages.Where(ui => ui.IsAvatar == true).FirstOrDefault()
                };
                people.Add(person);
            }
            return people;
        }
    }
}