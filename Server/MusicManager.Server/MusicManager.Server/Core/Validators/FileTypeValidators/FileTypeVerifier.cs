using MusicManager.Server.Core.Validators.FileTypeValidators.FileTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Validators.FileTypeValidators
{
    public static class FileTypeVerifier
    {
        private static FileTypeVerifyResult Unknown = new FileTypeVerifyResult
        {
            Name = "Unknown",
            Description = "Unknown File Type",
            IsVerified = false
        };

        static FileTypeVerifier()
        {
            Types = new List<FileType>
            {
                new Mp3()
            }
            .OrderByDescending(x => x.SignatureLength)
            .ToList();
        }

        private static IEnumerable<FileType> Types { get; set; }

        public static FileTypeVerifyResult What(Stream fileReadStream)
        {
            FileTypeVerifyResult result = null;

            foreach (var fileType in Types)
            {
                result = fileType.Verify(fileReadStream);
                if (result.IsVerified)
                    break;
            }

            return result?.IsVerified == true
               ? result
               : Unknown;
        }
    }
}
