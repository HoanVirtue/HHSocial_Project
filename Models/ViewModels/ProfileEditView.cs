using System.Net.Sockets;

namespace Clone_Main_Project_0710.Models.ViewModels
{
    public class ProfileEditView
    {
        public User? user { get; set; }
        public UserImage? userImage { get; set; }
        public ChangePasswordView changePasswordView { get; set; }
    }
}