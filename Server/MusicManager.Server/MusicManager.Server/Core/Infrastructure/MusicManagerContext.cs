using Microsoft.EntityFrameworkCore;
using MusicManager.Server.Core.Model;

namespace MusicManger.Server.Core.Infrastructure
{
    public class MusicManagerContext : DbContext, IMusicManagerContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<SongToPlaylist> SongToPlaylists { get; set; }
        public virtual DbSet<Song> Songs { get; set; }

        public MusicManagerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=localhost\SQLSERVER2019;Database=MusicManager;User Id=music_manager;Password=1337;Initial Catalog=MusicManager");
        }

    }
}