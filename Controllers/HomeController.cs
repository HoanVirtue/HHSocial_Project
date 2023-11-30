using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.DataSession;
using Clone_Main_Project_0710.Constant;
using Clone_Main_Project_0710.Repository;
using Clone_Main_Project_0710.DataCookies;
using Clone_Main_Project_0710.Models.ViewModels;

namespace Clone_Main_Project_0710.Controllers;

public class HomeController : Controller
{
    private UsersRepository _userContext;
    private UserImagesRepository _imageContext;
    private UserPostsRepository _postContext;
    public HomeController(SocialContext context)
    {
        _userContext = new UsersRepository(context);
        _imageContext = new UserImagesRepository(context);
        _postContext = new UserPostsRepository(context);
    }
    public async Task<IActionResult> IndexAsync()
    {
        string emailSess = Request.Cookies[UsersCookiesConstant.CookieEmail];
        if (emailSess == null)
        {
            return RedirectToAction("Index", "Users");
        }

        Guid userId = Guid.Parse(Request.Cookies[UsersCookiesConstant.CookieUserId]);

        User user = await _userContext.FindByID(userId);
        UserImage userImage = await _imageContext.GetAvatarByUserId(userId);
        List<PostView> listPost = await _postContext.GetAllPosts(userId);


        ProfileView view = new ProfileView()
        {
            User = user,
            ImageAvatar = userImage,
            UserPosts = listPost
        };

        return View(view);
    }
}
