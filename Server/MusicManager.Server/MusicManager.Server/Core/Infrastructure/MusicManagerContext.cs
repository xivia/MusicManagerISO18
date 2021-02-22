using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MusicManager.Server.Core.Config;
using MusicManager.Server.Core.Model;

namespace MusicManger.Server.Core.Infrastructure
{
    public class MusicManagerContext : DbContext
    {
        private AppSettings _appSettings;

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<PasswordResetLink> PasswordResetLinks { get; set; }
        public virtual DbSet<SongLyrics> SongLyrics { get; set; }

        public MusicManagerContext(DbContextOptions options, IOptions<AppSettings> appSettings) : base(options)
        {
            _appSettings = appSettings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_appSettings.ConnectionString);
        }

    }
}