using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Repository;
using HHSocialNetwork_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Clone_Main_Project_0710.Repository
{
    public class UserFriendsRepository : IRepository<UserFriend>
    {
        private SocialContext _context;
        private UsersRepository _userContext;
        public UserFriendsRepository (SocialContext context)
        {
            _context=context;
            _userContext = new UsersRepository(context);
        }
        public Task Add(UserFriend entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserFriend> FindByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserFriend>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserFriend>> GetAll(Guid targetId, string type)
        {
            
            List<UserFriend> listUserFriend = await _context.UserFriends.Where(m => m.TargetId.Equals(targetId) && m.IsFriend == false).ToListAsync();
            foreach(UserFriend u in listUserFriend) {
                u.SourceUser = await _userContext.FindByID(u.SourceId);
                u.TargetUser = await _userContext.FindByID(u.TargetId);
            }
            return listUserFriend;
        }
 
        public Task Update(UserFriend entity)
        {
            throw new NotImplementedException();
        }
    }
}