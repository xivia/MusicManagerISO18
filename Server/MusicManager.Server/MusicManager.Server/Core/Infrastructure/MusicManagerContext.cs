using Microsoft.EntityFrameworkCore;
using MusicManager.Server.Core.Model;

namespace MusicManger.Server.Core.Infrastructure
{
    public class MusicManagerContext : DbContext, IMusicManagerContext
    {
        public DbSet<User> Users;
        public DbSet<Genre> Genres;
        public DbSet<Playlist> Playlists;
        public DbSet<SongToPlaylist> SongToPlaylists;
        public DbSet<Song> Songs;

        public MusicManagerContext(DbContextOptions options) : base(options)
        {
    
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInternalServiceProvider(MySql)
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=musicmanager;Uid=music_manager;Pwd=1337;");
        }

    }
}