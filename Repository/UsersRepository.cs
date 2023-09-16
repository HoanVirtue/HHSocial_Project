using HHSocialNetwork_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace HHSocialNetwork_Project.Repository
{
    public class UsersRepository : IRepository<User>
    {
        private SocialContext _context;
        public UsersRepository(SocialContext context)
        {
            _context = context;
        }

        public async Task Add(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }

        public bool ExistByEmail(string email)
        {
            User? user = _context.Users.SingleOrDefault(p => p.Email.Equals(email));
            
            return user != null;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exist(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByID(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            
            return user;
        }

        public Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(User entity)
        {
            throw new NotImplementedException();
        }

    }
}