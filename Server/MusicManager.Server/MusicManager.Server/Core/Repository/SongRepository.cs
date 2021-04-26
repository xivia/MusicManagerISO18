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
        Task<List<Song>> SearchSongByTitle(string search);
    }

    public class SongRepository : GenericRepository<Song>, ISongRepository
    {
        public SongRepository(MusicManagerContext context) : base(context)
        {

        }

        public override async Task<Song> GetById(long songId)
        {
            return await _context.Songs.Where(song => song.SongId == songId).DefaultIfEmpty()
                    .Include(song => song.SongGenre)
                    .Include(song => song.Artist)
                    .FirstAsync();
        }

        public async Task<List<Song>> SearchSongByTitle(string search)
        {
            return await _context.Songs.Where(song => song.Name.Contains(search))
                .ToListAsync();
        }

    }
}
