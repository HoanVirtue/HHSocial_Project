using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Clone_Main_Project_0710.Repository
{
    public class UserPostsRepository : IRepository<UserPost>
    {
        public SocialContext _context;
        public UserImagesRepository _imageContext;
        public UserPostsRepository(SocialContext context)
        {
            _context = context;
            _imageContext = new UserImagesRepository(context);
        }
        public async Task Add(UserPost entity)
        {
            await _context.UserPosts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserPost> FindByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserPost>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(UserPost entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserPost>> GetMyPosts(Guid userId)
        {
            List<UserPost> listPost = await _context.UserPosts.Include(m => m.User)
                                                                .Where(m => m.UserId.Equals(userId))
                                                                .OrderByDescending(m => m.UpdatedAt).ToListAsync();

            return listPost;
        }

        public async Task<List<PostView>> GetMyPostsView(Guid userId)
        {
            List<PostView> postViews = new List<PostView>();
            List<UserPost> userPosts = await GetMyPosts(userId);
            foreach(UserPost post in userPosts)
            {
                PostView view = new PostView();
                view.UserPost = post;
                
                if(post.HasImage)
                {
                    List<UserImage> listImage = await _imageContext.GetImagesByPostId(post.UserPostId);
                    view.UserImage = listImage.FirstOrDefault();
                }
                postViews.Add(view);
            }

            return postViews;
        }
    }
}