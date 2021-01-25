using MusicManager.Server.Core.Model;
using System;

namespace MusicManager.Server.Core.DataTransferObjects.SongDtos
{
    public class SongResponseDto
    {
        public long SongId { get; set; }
        public Genre Genre { get; set; }
        public string Name { get; set; }
        public DateTime PublishOn { get; set; } = DateTime.Now;
        public UserResponseDto Artist { get; set; }
        public string CoverFileBase64 { get; set; }
    }
}
