using FluentValidation;
using MusicManager.Server.Core.DataTransferObjects.SongDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Validators
{
    public class SongRequestDtoValidator : AbstractValidator<SongRequestDto>
    {

        public SongRequestDtoValidator()
        {
            RuleFor(songRequestDto => songRequestDto.Name)
                .NotEmpty()
                .MaximumLength(64)
                .MinimumLength(1);
        }

    }
}
