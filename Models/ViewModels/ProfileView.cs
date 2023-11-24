namespace Clone_Main_Project_0710.Models.ViewModels
{
    public class ProfileView
    {
        public User? User { get; set; }
        public UserImage? ImageAvatar { get; set; }
        public List<PostView>? UserPosts { get; set; }
    }
}