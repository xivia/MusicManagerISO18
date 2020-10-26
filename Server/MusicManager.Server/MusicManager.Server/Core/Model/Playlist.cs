namespace MusicManager.Server.Core.Model
{
    public class Playlist
    {
        public int PlaylistId { get; set; }
        public User user { get; set; }
        public string Name { get; set; }
    }
}