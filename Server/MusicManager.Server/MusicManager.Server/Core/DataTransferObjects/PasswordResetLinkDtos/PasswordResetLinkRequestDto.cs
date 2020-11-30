using System;

namespace MusicManager.Server.Core.DataTransferObjects.PasswordResetLinkDtos
{
    public class PasswordResetLinkRequestDto
    {
        public Guid LinkGuid { get; set; }
        public string Password { get; set; }
    }
}
