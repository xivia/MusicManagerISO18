using FluentValidation;
using MusicManager.Server.Core.DataTransferObjects.PasswordResetLinkDtos;

namespace MusicManager.Server.Core.Validators
{
    public class PasswordResetLinkRequestDtoValidator : AbstractValidator<PasswordResetLinkRequestDto>
    {

        public PasswordResetLinkRequestDtoValidator()
        {
            RuleFor(passwordResetLinkRequestDto => passwordResetLinkRequestDto.LinkGuid)
                .NotEmpty();

            RuleFor(passwordResetLinkRequestDto => passwordResetLinkRequestDto.Password)
                .NotEmpty()
                    .WithMessage("Password can't be empty!")
                .MaximumLength(128)
                    .WithMessage("Password can't be longer than 128 characters!")
                .MinimumLength(8)
                    .WithMessage("Your password has to be atleast 8 characters long!");
        }

    }
}
