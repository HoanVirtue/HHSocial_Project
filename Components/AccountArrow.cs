using Clone_Main_Project_0710.Constant;
using Clone_Main_Project_0710.DataCookies;
using Clone_Main_Project_0710.DataSession;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using Clone_Main_Project_0710.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Clone_Main_Project_0710.Components
{
    public class AccountArrow : ViewComponent
    {
        private UsersRepository _context;
        private UserImagesRepository _imageContext;
        public AccountArrow(SocialContext context)
        {
            _context = new UsersRepository(context);
            _imageContext = new UserImagesRepository(context);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Guid userId = UsersCookies.GetUserCookie().UserId;

            ProfileEditView view = new ProfileEditView();
            User user = await _context.FindByID(userId);
            UserImage image = await _imageContext.GetAvatarByUserId(user.UserId);

            view.User = user;
            view.UserImage = image;
            return View(view);
        }
    }
}