using FluentValidation;
using Microsoft.AspNetCore.Http;
using MusicManager.Server.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Services
{
    public interface IFileService
    {
        Task<FileDto> UploadFile(IFormFile file, string uploadDirectory, AbstractValidator<IFormFile> validator);
        bool DeleteFile(string filePath);
    }

    public class FileService : IFileService
    {

        public async Task<FileDto> UploadFile(IFormFile file, string uploadDirectory, AbstractValidator<IFormFile> validator)
        {
            var response = new FileDto();

            try
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                var validationResult = validator.Validate(file);

                if(!validationResult.IsValid)
                    throw new Exception(string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage)));

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
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public bool DeleteFile(string filePath)
        {
            var isDeleted = false;

            if(File.Exists(filePath))
            {
                File.Delete(filePath);
                isDeleted = true;
            }

            return isDeleted;
        }

    }
}
