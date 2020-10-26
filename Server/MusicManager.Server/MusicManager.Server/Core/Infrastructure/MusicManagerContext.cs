using Microsoft.EntityFrameworkCore;
using MusicManager.Server.Core.Model;

namespace MusicManger.Server.Core.Infrastructure
{
    public class MusicManagerContext : DbContext
    {
        public DbSet<User> Users;
        public DbSet<Genre> Genres;
        public DbSet<Playlist> Playlists;
        public DbSet<SongToPlaylist> SongToPlaylists;
        public DbSet<Song> Songs;

        public MusicManagerContext(DbContextOptions<MusicManagerContext> options) : base(options)
        {
         
        }
    }
}