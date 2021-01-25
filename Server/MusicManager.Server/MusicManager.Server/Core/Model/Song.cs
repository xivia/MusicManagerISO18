using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManager.Server.Core.Model
{
    public class Song
    {
        [Key]
        public long SongId { get; set; }

        [ForeignKey("GenreId")]
        public long GenreId { get; set; }
        public Genre SongGenre { get; set; }

        [ForeignKey("UserId")]
        public long UserId { get; set; }
        public User Artist { get; set; }

        public string Name { get; set; }

        public string FilePath { get; set; }

        public bool Deleted { get; set; }

        public DateTime PublishOn { get; set; }

        public string CoverFilePath { get; set; }
    }
}