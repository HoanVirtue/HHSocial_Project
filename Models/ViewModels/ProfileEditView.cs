using System.Net.Sockets;

namespace Clone_Main_Project_0710.Models.ViewModels
{
    public class ProfileEditView
    {
        public User? User { get; set; }
        public UserImage? UserImage { get; set; }
        public ChangePasswordView ChangePasswordView { get; set; }
    }
}