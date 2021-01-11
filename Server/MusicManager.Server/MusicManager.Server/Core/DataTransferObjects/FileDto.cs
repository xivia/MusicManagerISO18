using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.DataTransferObjects
{
    public class FileDto : BaseResponseDto
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
