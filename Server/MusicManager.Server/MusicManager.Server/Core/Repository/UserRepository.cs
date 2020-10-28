using MusicManager.Server.Core.Model;
using MusicManger.Server.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Repository
{
    public class UserRepository : IRepository<User>
    {
        #region Injectables
        private IMusicManagerContext _context;
        #endregion

        public UserRepository(IMusicManagerContext context)
        {
            _context = context;
        }

        #region Interface Implementations
        public Task<User> Delete(User value)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Insert(User value)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User value)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
