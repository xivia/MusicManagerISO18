namespace MusicManger.Server.Core.Model
{
    public class Song
    {
        public int SongId { get; set; }
        public Genre SongGenre { get; set; }
        public User Artist { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public bool Deleted { get; set; }
        public DateTime PublishOn { get; set; }
    }
}