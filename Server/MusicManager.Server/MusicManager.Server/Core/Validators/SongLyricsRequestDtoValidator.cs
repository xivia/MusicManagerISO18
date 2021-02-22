using FluentValidation;
using MusicManager.Server.Core.DataTransferObjects.SongLyricsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Validators
{
    public class SongLyricsRequestDtoValidator : AbstractValidator<SongLyricsRequestDto>
    {
        public SongLyricsRequestDtoValidator()
        {
            RuleFor(x => x.SongLyrics)
                .NotEmpty()
                .Length(1, 32767); // 32767 = max length for nvarchar(max)

            RuleFor(x => x.SongId)
                .NotEmpty();
        }
    }
}
