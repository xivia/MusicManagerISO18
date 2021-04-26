using MusicManager.Server.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.DataTransferObjects.PlaylistDtos
{
    public class PlaylistDto
    {
        public long PlaylistId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public virtual List<Song> Songs { get; set; }
    }
}
