using Clone_Main_Project_0710.Models;
using Microsoft.EntityFrameworkCore;

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

        public Task Update(UserImage entity)
        {
            throw new NotImplementedException();
        }

        public async Task<UserImage> GetAvatarByUserId(Guid userId)
        {
            UserImage image = await _context.UserImages.Where(m => m.UserId == userId && m.IsAvatar == true).FirstOrDefaultAsync();
            return image;
        }
    }
}