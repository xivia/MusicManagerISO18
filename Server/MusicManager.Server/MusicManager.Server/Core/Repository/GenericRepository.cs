using Microsoft.EntityFrameworkCore;
using MusicManager.Server.Core.Model;
using MusicManger.Server.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected MusicManagerContext _context;
        private DbSet<T> _entities;

        public GenericRepository(MusicManagerContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<bool> Delete(T value)
        {
            bool isDeleted = false;

            if (_entities.Contains(value))
            {
                _entities.Remove(value);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }

            return isDeleted;
        }

        public async Task<List<T>> GetAll()
        {
            return await _entities
                            .Select(x => x)
                            .ToListAsync();
        }

        public async Task<T> GetById(long id)
        {
            return await _entities
                            .Where(entity => (long) entity
                                .GetType()
                                .GetProperty($"{typeof(T).Name}Id")
                                .GetValue(entity, null) == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<T> Insert(T value)
        {
            _entities.Add(value);
            await _context.SaveChangesAsync();
            return value;
        }

        public async Task<bool> Update(T value)
        {
            bool isUpdated = false;

            var dbEntity = await _entities
                            .FirstOrDefaultAsync(entity => 
                                (long) entity
                                    .GetType()
                                    .GetProperty($"{typeof(T).Name}Id")
                                    .GetValue(entity, null) 
                                    == 
                                (long) value.GetType()
                                    .GetProperty($"{typeof(T).Name}Id")
                                    .GetValue(entity, null));

            if (!(dbEntity is null))
            {
                _context.Entry(dbEntity).CurrentValues.SetValues(value);
                await _context.SaveChangesAsync();
                isUpdated = true;
            }

            return isUpdated;
        }
    }
}
