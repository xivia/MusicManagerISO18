using FluentValidation;
using MusicManager.Server.Core.DataTransferObjects.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty()
                    .WithMessage("Username can't be empty!")
                .MaximumLength(14)
                    .WithMessage("Username can't be longer than 14 characters!");
            RuleFor(user => user.Password)
                .NotEmpty()
                    .WithMessage("Password can't be empty!")
                .MaximumLength(128)
                    .WithMessage("Password can't be longer than 128 characters!")
                .MinimumLength(8)
                    .WithMessage("Your password has to be atleast 8 characters long!");
                //.Matches(@"^(?!.([A-Za-z0-9])\1{1})(?=.?[A-Z])(?=.?[a-z])(?=.?[0-9])(?=.?[#?!@$%^&-]).{8,}$")
                //    .WithMessage("Your password has to be atleast 8 characters long, contain atleast 1 unique char, contain atleast 1 digit, 1 lowercase character, 1 upper case character and 1 special character.");
        }
    }
}
