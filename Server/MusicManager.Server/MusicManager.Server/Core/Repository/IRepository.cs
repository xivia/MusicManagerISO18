using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> Insert(T value);
        Task<bool> Update(T value);
        Task<bool> Delete(T value);
        Task<T> GetById(long id);
        Task<List<T>> GetAll();
    }
}
