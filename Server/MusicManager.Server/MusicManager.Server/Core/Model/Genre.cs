using System.ComponentModel.DataAnnotations;

namespace MusicManager.Server.Core.Model
{
    public class Genre
    {
        [Key]
        public long GenreId { get; set; }
        public string Name { get; set; }
    }
}