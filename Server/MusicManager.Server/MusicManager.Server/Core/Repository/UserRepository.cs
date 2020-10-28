using Microsoft.EntityFrameworkCore;
using MusicManager.Server.Core.Model;
using MusicManger.Server.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Repository
{
    public interface IUserRepository : IRepository<User>
    {

    }

    public class UserRepository : IUserRepository
    {
        #region Injectables
        private MusicManagerContext _context;
        #endregion

        public UserRepository(MusicManagerContext context)
        {
            _context = context;
        }

        #region Interface Implementations
        public async Task<bool> Delete(User user)
        {
            bool isDeleted = false;

            if (_context.Users.Contains(user))
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }

            return isDeleted;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users
                            .Select(x => x)
                            .ToListAsync();
        }

        public async Task<User> GetById(long id)
        {
            return await _context.Users
                            .Where(user => user.UserId == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<User> Insert(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            
            if(!(dbUser is null))
            {
                _context.Entry(dbUser).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
            }

            return dbUser;
        }
        #endregion
    }
}
