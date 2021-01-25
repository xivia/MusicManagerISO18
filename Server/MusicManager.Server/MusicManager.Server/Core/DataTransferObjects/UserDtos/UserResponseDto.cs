using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.DataTransferObjects
{
    public class UserResponseDto
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public bool Banned { get; set; }
    }
}
