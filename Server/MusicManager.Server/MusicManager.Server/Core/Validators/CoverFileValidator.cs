using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Validators
{
    public class CoverFileValidator : AbstractValidator<IFormFile>
    {
        private const int MAX_FILE_SIZE_MB = 5;
        private List<string> _allowedFileTypes = new List<string> { "image/jpeg", "image/png" };

        public CoverFileValidator()
        {
            // length / 1048576 gives size in mb
            RuleFor(x => x.Length / 1048576).NotNull().LessThanOrEqualTo(MAX_FILE_SIZE_MB)
                .WithMessage($"File size is larger than max allowed size {MAX_FILE_SIZE_MB}");

            RuleFor(x => x.ContentType).NotNull().Must(x => _allowedFileTypes.Contains(x))
                .WithMessage($"File type is not allowed, allowed are {string.Join(", ", _allowedFileTypes)}");
        }
    }
}
