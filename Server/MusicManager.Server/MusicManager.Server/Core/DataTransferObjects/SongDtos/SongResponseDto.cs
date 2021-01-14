using MusicManager.Server.Core.DataTransferObjects.UserDtos;
using MusicManager.Server.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.DataTransferObjects.SongDtos
{
    public class SongResponseDto
    {
        public Genre Genre { get; set; }
        public string Name { get; set; }
        public DateTime PublishOn { get; set; } = DateTime.Now;
        public UserResponseDto Artist { get; set; }
    }
}
