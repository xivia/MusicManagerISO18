using FluentValidation;
using MusicManager.Server.Core.DataTransferObjects.GenreDtos;

namespace MusicManager.Server.Core.Validators
{
    public class GenreValidator : AbstractValidator<GenreRequestDto>
    {

        public GenreValidator()
        {
            RuleFor(genre => genre.GenreName)
                .NotEmpty()
                .MaximumLength(24)
                .MinimumLength(3);
        }

    }
}
