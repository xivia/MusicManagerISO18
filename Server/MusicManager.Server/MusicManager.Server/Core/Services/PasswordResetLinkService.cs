using Microsoft.Extensions.Configuration;
using MusicManager.Server.Core.DataTransferObjects;
using MusicManager.Server.Core.DataTransferObjects.PasswordResetLinkDtos;
using MusicManager.Server.Core.Model;
using MusicManager.Server.Core.Repository;
using MusicManager.Server.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Services
{
    public interface IPasswordResetLinkService
    {
        Task GenerateResetLink(User user);
        Task<BaseResponseDto> IsLinkValid(Guid linkGuid);
        Task<BaseResponseDto> ResetPassword(PasswordResetLinkRequestDto passwordResetLinkRequestDto);
    }

    public class PasswordResetLinkService : IPasswordResetLinkService
    {

        private readonly IPasswordResetLinkRepository _passwordResetLinkRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public PasswordResetLinkService(IPasswordResetLinkRepository passwordResetLinkRepository,
            IUserRepository userRepository,
            IEmailService emailService, 
            IConfiguration configuration)
        {
            _passwordResetLinkRepository = passwordResetLinkRepository;
            _userRepository = userRepository;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task GenerateResetLink(User user)
        {
            var passwordResetLink = new PasswordResetLink
            {
                LinkGuid = Guid.NewGuid(),
                User = user
            };

            await _emailService.SendEmail(
                "password-reset@music-manager.cc",
                user.EmailAddress,
                "Password Reset",
                $"Hello {user.Name} your account has been locked due to suspicious activity after 3 failed login attempts. Your password reset link is {_configuration["Hostname"]}/reset/{passwordResetLink.LinkGuid}"
            );

            await _passwordResetLinkRepository.Insert(passwordResetLink);
        }

        public async Task<BaseResponseDto> IsLinkValid(Guid linkGuid)
        {
            var response = new BaseResponseDto();

            var dbPasswordResetLink = await _passwordResetLinkRepository.GetResetLinkByLinkGuid(linkGuid);

            if(dbPasswordResetLink is null)
            {
                response.Infos.Errors.Add("Invalid reset link!");
                response.StatusCode = HttpStatusCode.NotFound;
            }

            return response;
        }

        public async Task<BaseResponseDto> ResetPassword(PasswordResetLinkRequestDto passwordResetLinkRequestDto)
        {
            var response = await IsLinkValid(passwordResetLinkRequestDto.LinkGuid);

            if (response.StatusCode != HttpStatusCode.OK) return response;

            var dbPasswordResetLink = await _passwordResetLinkRepository.GetResetLinkByLinkGuid(passwordResetLinkRequestDto.LinkGuid);
            User dbUser = dbPasswordResetLink.User;

            PasswordResetLinkRequestDtoValidator passwordResetLinkRequestDtoValidator = new PasswordResetLinkRequestDtoValidator();
            var validationResult = await passwordResetLinkRequestDtoValidator.ValidateAsync(passwordResetLinkRequestDto);

            if(!validationResult.IsValid)
            {
                response.Infos.Errors.AddRange(validationResult.Errors.Select(result => result.ErrorMessage));
                response.StatusCode = HttpStatusCode.UnprocessableEntity;
                return response;
            }

            dbUser.FailedLoginAttempts = 0;
            dbUser.Password = passwordResetLinkRequestDto.Password;

            await _userRepository.Update(dbUser);

            await _passwordResetLinkRepository.Delete(dbPasswordResetLink);

            response.Infos.Messages.Add("Successfully changed the password");

            return response;
        }

    }
}
