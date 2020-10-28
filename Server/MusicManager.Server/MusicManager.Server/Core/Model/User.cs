using System.ComponentModel.DataAnnotations;

namespace MusicManager.Server.Core.Model
{
    public class User
    {
        [Key]
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Banned { get; set; }
    }
}