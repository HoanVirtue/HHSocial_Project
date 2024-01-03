using BTL.Models.ViewModels;
using HHSocialNetwork_Project.Models;

namespace Clone_Main_Project_0710.Models.ViewModels
{
    public class ProfileView
    {
        public User? User { get; set; }
        public UserImage? ImageAvatar { get; set; }
        public List<PostView>? UserPosts { get; set; }
        public List<FriendRequestView>? UserFriends { get; set; }
        public List<UserImage>? UserImages { get; set; }
    }
}