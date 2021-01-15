using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManager.Server.Core.Model
{
    public class SongToPlaylist
    {
        // TODO: Remove this class and add List<Song> songs to Playlist
        [Key]
        public long SongToPlaylistId { get; set; }
        [ForeignKey("PlaylistId")]
        public Playlist SongToPlaylistPlaylist { get; set; }
        [ForeignKey("SongId")]
        public Song PlaylistSong { get; set; }
        public bool Deleted { get; set; }
    }
}
