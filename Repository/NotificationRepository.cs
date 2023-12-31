using System.Resources;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Repository;
using Microsoft.EntityFrameworkCore;

namespace Clone_Main_Project_0710
{
    public class NotifitcationRepository : IRepository<Notification>
    {
        private SocialContext _context;
        private UserImagesRepository _imageContext;
        public NotifitcationRepository(SocialContext context)
        {
            _context = context;
            _imageContext = new UserImagesRepository(context);
        }
        public async Task Add(Notification entity)
        {
            _context.Notifications.Add(entity);
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

        public Task<Notification> FindByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notification>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(Notification entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Notification>> GetNotificationsByUserId(Guid userId)
        {
            List<Notification> notifications = await _context.Notifications.Where(m => m.TargetId.Equals(userId))
                                                                    .Include(m => m.SourceUser)
                                                                    .Include(m => m.TargetUser)
                                                                    .Include(m => m.UserPost)
                                                                    .ToListAsync();

            return notifications;
        }
    }
}