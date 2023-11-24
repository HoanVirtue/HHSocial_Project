using Clone_Main_Project_0710.DataSession;
using Clone_Main_Project_0710.GendericMethod;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using Clone_Main_Project_0710.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Clone_Main_Project_0710.Controllers
{
    public class UserPostsController : Controller
    {
        private UserPostsRepository _postContext;
        private UserImagesRepository _imageContext;

        public UserPostsController(SocialContext context)
        {
            _postContext = new UserPostsRepository(context);
            _imageContext = new UserImagesRepository(context);
        }

        private Guid userIdTest = new Guid("f98d25ef-75b8-4f1e-8e09-9281d33e8f32");
        private string emailTest = "hoan@gmail.com";
        private void SetSession()
        {
            HttpContext.Session.SetString(DataSession.SessionData.USERID_SESS, userIdTest.ToString());
            HttpContext.Session.SetString(DataSession.SessionData.USER_EMAIL_SESS, emailTest);
        }

        public async Task<JsonResult> CreatePost(string Content, bool HasImage, IFormFile ImagePost)
        {
            SetSession();
            bool isSuccess = true;
            UserPost post = null;
            UserImage userImage = null;
            string errorMsg = "";
            try
            {
                Guid userId = Guid.Parse(HttpContext.Session.GetString(SessionData.USERID_SESS));
                post = new UserPost()
                {
                    UserPostId = Guid.NewGuid(),
                    UserId = userId,
                    Content = Content,
                    Likes = 0,
                    Comments = 0,
                    HasImage = HasImage,
                    Privacy = "public",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _postContext.Add(post);

                if (HasImage && ImagePost != null && ImagePost.Length > 0)
                {
                    userImage = new UserImage
                    {
                        UserId = userId,
                        UserPostId = post.UserPostId,
                        ImageName = ImagePost.FileName,
                        ImageData = ImageMethod.ConvertImageToString(ImagePost),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    await _imageContext.Add(userImage);
                }
            } catch(Exception e) {
                isSuccess = false;
                errorMsg = e.Message;
            }


            return Json(new {
                isSuccess = isSuccess,
                error = errorMsg,
                post = post,
                imagePost = userImage
            });
        }
    }
}