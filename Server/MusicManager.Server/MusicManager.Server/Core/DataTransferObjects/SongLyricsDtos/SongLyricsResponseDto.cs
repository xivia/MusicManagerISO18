using MusicManager.Server.Core.DataTransferObjects.SongDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.DataTransferObjects.SongLyricsDtos
{
    public class SongLyricsResponseDto
    {
        public long SongLyricsId { get; set; }
        public string SongLyrics { get; set; }
        public SongResponseDto Song { get; set; }
    }
}
