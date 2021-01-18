using FluentValidation;
using Microsoft.AspNetCore.Http;
using MusicManager.Server.Core.Validators.FileTypeValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Validators
{
    public class SongFileValidator : AbstractValidator<IFormFile>
    {
        private const int MAX_FILE_SIZE_MB = 50;

        public SongFileValidator()
        {
            // TODO: Validate file header signature
            RuleFor(x => x).Custom((file, context) => 
            {
                var result = FileTypeVerifier.What(file.OpenReadStream());
                
                if(!result.IsVerified)
                    context.AddFailure("The file has an invalid file type");
            });

            // length / 1048576 gives size in mb
            RuleFor(x => x.Length / 1048576).NotNull().LessThanOrEqualTo(MAX_FILE_SIZE_MB)
                .WithMessage($"File size is larger than max allowed size {MAX_FILE_SIZE_MB}");

            RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("audio/mpeg"))
                .WithMessage("File type is not allowed");
        }

    }
}
