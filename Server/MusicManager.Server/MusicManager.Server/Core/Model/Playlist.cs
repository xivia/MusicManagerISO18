using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManager.Server.Core.Model
{
    public class Playlist
    {
        [Key]
        public long PlaylistId { get; set; }
        [ForeignKey("UserId")]
        public User user { get; set; }
        public string Name { get; set; }
    }
}