using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Validators
{
    public class SongFileValidator : AbstractValidator<IFormFile>
    {
        private const int MAX_FILE_SIZE = 5000;

        public SongFileValidator()
        {
            RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(MAX_FILE_SIZE)
                .WithMessage($"File size is larger than max allowed size {MAX_FILE_SIZE}");

            RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("audio/mpeg"))
                .WithMessage("File type is not allowed");
        }

    }
}
