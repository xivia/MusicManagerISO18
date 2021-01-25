using Microsoft.EntityFrameworkCore;
using MusicManager.Server.Core.Model;
using MusicManger.Server.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Repository
{
    public interface ISongRepository : IRepository<Song>
    {

    }

    public class SongRepository : GenericRepository<Song>, ISongRepository
    {
        private readonly MusicManagerContext _context;

        public SongRepository(MusicManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Song> GetById(long songId)
        {
            return await _context.Songs.Where(song => song.SongId == songId)
                    .Include(song => song.SongGenre)
                    .Include(song => song.Artist)
                    .FirstAsync();
        }

    }
}
