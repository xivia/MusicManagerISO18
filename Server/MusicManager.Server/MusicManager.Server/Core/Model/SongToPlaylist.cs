using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Model
{
    public class SongToPlaylist
    {
        public Playlist SongToPlaylistPlaylist { get; set; }
        public List<Song> Songs { get; set; }
    }
}
