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
        User GetByUsername(string username);
    }

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        #region Injectables
        private MusicManagerContext _context;
        #endregion

        public UserRepository(MusicManagerContext context) : base(context)
        {
            _context = context;
        }

        public User GetByUsername(string username)
        {
            return _context.Users
               .Where(user => user.Name == username)
               .FirstOrDefault();
        }

    }
}
