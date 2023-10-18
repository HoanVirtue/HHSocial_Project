using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.Repository;
using HHSocialNetwork_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace HHSocialNetwork_Project.Repository
{
    public class UserFriendsRepository : IRepository<UserFriend>
    {
        private SocialContext _context;
        public UserFriendsRepository (SocialContext context){
                _context=context;
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
            List<UserFriend> listuserfriends = await _context.UserFriends.ToListAsync();
            return listuserfriends;
        }

        public Task Update(UserFriend entity)
        {
            throw new NotImplementedException();
        }
    }
}