using System;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.Server.Core.Model
{
    public class PasswordResetLink
    {
        [Key]
        public long PasswordResetLinkID { get; set; }
        public User User { get; set; }
        public Guid LinkGuid { get; set; }
    }
}
