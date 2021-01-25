using Microsoft.EntityFrameworkCore;
using MusicManager.Server.Core.Model;
using MusicManger.Server.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Repository
{
    public interface ISongLyricsRepository : IRepository<SongLyrics>
    {
        Task<SongLyrics> GetBySongId(long songId);
    }

    public class SongLyricsRepository : GenericRepository<SongLyrics>, ISongLyricsRepository
    {

        public SongLyricsRepository(MusicManagerContext context) : base(context)
        {

        }

        public async Task<SongLyrics> GetBySongId(long songId)
        {
            return await _context.SongLyrics
                .Where(songLyrics => songLyrics.Song.SongId == songId)
                .Include(songLyrics => songLyrics.Song)
                .FirstOrDefaultAsync();
        }
    }
}
