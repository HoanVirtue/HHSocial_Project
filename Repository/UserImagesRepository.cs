using Clone_Main_Project_0710.Controllers;
using Clone_Main_Project_0710.Models;
using Microsoft.EntityFrameworkCore;
using Clone_Main_Project_0710.Controllers;
using HHSocialNetwork_Project.Models;

namespace Clone_Main_Project_0710.Repository
{
    public class UserImagesRepository : IRepository<UserImage>
    {
        private SocialContext _context;
        public UserImagesRepository(SocialContext context)
        {
            _context = context;
        }
        public async Task Add(UserImage entity)
        {
            _context.UserImages.Add(entity);
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

        public Task<UserImage> FindByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserImage>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateByUserId(UserImage entity)
        {
            UserImage image = await _context.UserImages.SingleOrDefaultAsync(m => m.UserId.Equals(entity.UserId) && m.IsAvatar);
            if (image != null)
            {
                image.ImageName = entity.ImageName;
                image.ImageData = entity.ImageData;
                image.IsAvatar = entity.IsAvatar;
                image.IsCoverImage = entity.IsCoverImage;
                image.UpdatedAt = entity.UpdatedAt;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserImage> GetAvatarByUserId(Guid userId)
        {
            UserImage image = await _context.UserImages.SingleOrDefaultAsync(m => m.UserId.Equals(userId) && m.IsAvatar == true);
            return image;
        }

        public Task Update(UserImage entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserImage>> GetImagesByPostId(Guid postId)
        {
            List<UserImage> userImages = await _context.UserImages.Where(m => m.UserPostId.Equals(postId))
                                            .OrderByDescending(m => m.UpdatedAt)
                                            .ToListAsync();

            return userImages;
        }

        public async Task<List<UserImage>> GetImagesByUserId(Guid userId)
        {
            List<UserImage> userImages = await _context.UserImages.Where(m => m.UserId.Equals(userId))
                                            .OrderByDescending(m => m.UpdatedAt)
                                            .ToListAsync();

            return userImages;
        } 
    }
}