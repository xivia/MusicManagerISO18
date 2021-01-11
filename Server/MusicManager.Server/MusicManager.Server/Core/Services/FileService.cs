using Microsoft.AspNetCore.Http;
using MusicManager.Server.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Services
{
    public interface IFileService
    {
        Task<BaseResponseDto> UploadFile(IFormFile file, string uploadDirectory);
    }

    public class FileService : IFileService
    {

        public async Task<BaseResponseDto> UploadFile(IFormFile file, string uploadDirectory)
        {
            var response = new FileDto();

            try
            {
                var fileName = $"{Guid.NewGuid()}.{Path.GetExtension(file.FileName)}";

                if(!Directory.Exists(uploadDirectory))
                    Directory.CreateDirectory(uploadDirectory);

                var filePath = Path.Combine(uploadDirectory, fileName);

                using (var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                response.FilePath = filePath;
                response.FileName = fileName;
            }
            catch (Exception e)
            {
                response.Infos.Errors.Add(e.Message);
            }

            return response;
        }

    }
}
