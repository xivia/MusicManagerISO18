namespace MusicManger.Server.Core.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Banned { get; set; }
    }
}