namespace MusicManger.Server.Core.Model
{
    public class Genre
    {
        public int PlaylistId { get; set; }
        public List<Song> Songs { get; set; }
        public User user { get; set; }
        public string Name { get; set; }
    }
}