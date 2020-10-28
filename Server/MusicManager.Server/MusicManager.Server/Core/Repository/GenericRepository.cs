using Microsoft.EntityFrameworkCore;
using MusicManager.Server.Core.Model;
using MusicManger.Server.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Repository
{
    public class GenericRepository<T> //: IRepository<T> where T : BaseEntity
    {
        //protected readonly DbContext _context;
        //private DbSet<T> _entities;
        //string errorMessage = string.Empty;
        //public Repository(IMusicManagerContext context)
        //{
        //    _context = (DbContext) context;
        //    _entities = _context.Set<T>();
        //}
        //public IEnumerable<T> GetAll()
        //{
        //    return _entities.AsEnumerable();
        //}

        //public void Delete(Guid id)
        //{
        //    if (id == null) throw new ArgumentNullException("entity");

        //    T entity = entities.SingleOrDefault(s => s.Id == id);
        //    entities.Remove(entity);
        //    context.SaveChanges();
        //}

        //Task IRepository<T>.Insert(T value)
        //{
        //    if (value == null) throw new ArgumentNullException("entity");

        //    _entities.Add(value);
        //    await _context.SaveChangesAsync();
        //}

        //Task IRepository<T>.Update(T value)
        //{
        //    if (entity == null) throw new ArgumentNullException("entity");
        //    _context.SaveChanges();
        //}

        //public Task Delete(T value)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task GetById(long id)
        //{
            
        //}


        //Task<T> IRepository<T>.Insert(T value)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<T> IRepository<T>.Update(T value)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<T> IRepository<T>.Delete(T value)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<T> IRepository<T>.GetById(long id)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<List<T>> IRepository<T>.GetAll()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
