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

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        #region Injectables
        private MusicManagerContext _context;
        #endregion

        public UserRepository(MusicManagerContext context) : base(context)
        {
            _context = context;
        }

    }
}
