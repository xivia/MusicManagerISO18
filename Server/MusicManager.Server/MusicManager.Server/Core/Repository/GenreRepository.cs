using MusicManager.Server.Core.Model;
using MusicManger.Server.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Repository
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Genre GetByName(string name);
    }

    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MusicManagerContext context) : base(context)
        {

        }

        public Genre GetByName(string name)
        {
            return _context.Genres.Where(genre => genre.Name == name).FirstOrDefault();
        }
    }
}
