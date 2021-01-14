using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.DataTransferObjects.SongDtos
{
    public class SongRequestDto
    {
        public long GenreId { get; set; }
        public string Name { get; set; }
        public DateTime PublishOn { get; set; } = DateTime.Now;
    }
}
