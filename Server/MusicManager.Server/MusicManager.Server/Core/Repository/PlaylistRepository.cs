using Microsoft.EntityFrameworkCore;
using MusicManager.Server.Core.Model;
using MusicManger.Server.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Repository
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
    }

    public class PlaylistRepository : GenericRepository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(MusicManagerContext context) : base(context)
        {

        }

        public override async Task<Playlist> GetById(long Id)
        {
            return await _context.Playlists.Where(p => p.PlaylistId == Id).DefaultIfEmpty()
                .Include(p => p.User).FirstAsync();
        }
    }
}
