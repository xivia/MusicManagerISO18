using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using MusicManager.Server.Core.DataTransferObjects;
using MusicManager.Server.Core.DataTransferObjects.Mapper;
using MusicManager.Server.Core.DataTransferObjects.UserDtos;
using MusicManager.Server.Core.Model;
using MusicManager.Server.Core.Repository;
using MusicManager.Server.Core.Validators;

namespace MusicManager.Server.Controller
{
    [Route("api/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ActionResult<BaseResponseDto>> Get()
        {
            var responseDto = new BaseResponseDto();

            List<User> users = await _userRepository.GetAll();

            responseDto.Data.Add("users", UserResponseDtoMapper.FromDb(users));

            return StatusCode((int) responseDto.StatusCode, responseDto);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<BaseResponseDto>> GetById(long userId)
        {
            var responseDto = new BaseResponseDto();

            User user = await _userRepository.GetById(userId);

            if(user is null) responseDto.StatusCode = HttpStatusCode.NotFound;

            responseDto.Data.Add("user", UserResponseDtoMapper.FromDb(user));

            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponseDto>> Create([FromBody] UserDto userDto)
        {
            var responseDto = new BaseResponseDto();

            UserDtoValidator userDtoValidator = new UserDtoValidator();
            ValidationResult validationResult = userDtoValidator.Validate(userDto);

            if(validationResult.IsValid)
            {
                var dbUser = _userRepository.GetByUsername(userDto.UserName);

                if (dbUser is null)
                {
                    User user = new User
                    {
                        Name = userDto.UserName,
                        Password = userDto.Password,
                        Banned = false
                    };

                    UserResponseDto userResponseDto = UserResponseDtoMapper.FromDb(await _userRepository.Insert(user));

                    responseDto.Infos.Messages.Add("Successfully created user");
                    responseDto.Data.Add("user", userResponseDto);
                }
                else 
                {
                    responseDto.Infos.Errors.Add("A user with that username exists already.");
                    responseDto.StatusCode = HttpStatusCode.Conflict;
                }
            }  
            else
            {
                var joinedErrors = validationResult.Errors.Join(";");
                responseDto.Infos.Errors.AddRange(joinedErrors.Split(";").ToList());
                responseDto.StatusCode = HttpStatusCode.BadRequest;
            }

            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

    }
}
