using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManager.Server.Core.Model
{
    public class Playlist
    {
        [Key]
        public long PlaylistId { get; set; }
        
        [ForeignKey("UserId")]
        public long UserId { get; set; }
        
        public User User { get; set; }
        
        public string Name { get; set; }
        
        public virtual List<Song> Songs { get; set; }
    }
}