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
        Task<User> GetByUsername(string username);
    }

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MusicManagerContext context) : base(context)
        {
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users
               .Where(user => user.Name == username)
               .FirstOrDefaultAsync();
        }

    }
}
