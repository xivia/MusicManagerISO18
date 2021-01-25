using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Model
{
    public class SongLyrics
    {
        [Key]
        public long SongLyricsId { get; set; }

        [ForeignKey("SongId")]
        public Song Song { get; set; }

        [MaxLength]
        public string Lyrics { get; set; }
    }
}
