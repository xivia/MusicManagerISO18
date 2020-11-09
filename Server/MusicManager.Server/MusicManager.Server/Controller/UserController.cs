using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        private IConfiguration _config;

        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _config = configuration;
        }

        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> Get()
        {
            var responseDto = new BaseResponseDto();

            List<User> users = await _userRepository.GetAll();

            responseDto.Data.Add("users", UserResponseDtoMapper.FromDb(users));

            return StatusCode((int) responseDto.StatusCode, responseDto);
        }

        [Authorize]
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
                var dbUser = await _userRepository.GetByUsername(userDto.UserName);

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
                    responseDto.Data.Add("token", GenerateJSONWebToken(dbUser));
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

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponseDto>> Login([FromBody] UserDto user)
        {
            var responseDto = new BaseResponseDto();
            responseDto.StatusCode = HttpStatusCode.Unauthorized;

            var dbUser = await _userRepository.GetByUsername(user.UserName);

            if(dbUser is null) return StatusCode((int)responseDto.StatusCode, responseDto);
            
            if(dbUser.Password != user.Password) return StatusCode((int)responseDto.StatusCode, responseDto);
            
            if(dbUser.Banned)
            {
                responseDto.Infos.Errors.Add("You are banned!");
                return StatusCode((int)responseDto.StatusCode, responseDto);
            }


            var token = GenerateJSONWebToken(dbUser);
            responseDto.Data.Add("Token", token);
            responseDto.StatusCode = HttpStatusCode.OK;

            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

        [Authorize]
        [HttpPut("{userId}/ban")]
        public async Task<ActionResult<BaseResponseDto>> Ban(long userId) 
        {
            var responseDto = await BanOrUnbanUser(userId, true);
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

        [Authorize]
        [HttpPut("{userId}/unban")]
        public async Task<ActionResult<BaseResponseDto>> Unban(long userId)
        {
            var responseDto = await BanOrUnbanUser(userId, false);
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

        private async Task<BaseResponseDto> BanOrUnbanUser(long userId, bool ban)
        {
            var responseDto = new BaseResponseDto();

            var dbUser = await _userRepository.GetById(userId);

            if (dbUser != null)
            {
                dbUser.Banned = true;

                await _userRepository.Update(dbUser);

                string text = ban ? "banned" : "unbanned";

                responseDto.Infos.Messages.Add($"User with id {userId} has successfully been {text}");
            }
            else
            {
                responseDto.Infos.Errors.Add($"User with id {userId} has not been found.");
                responseDto.StatusCode = HttpStatusCode.NotFound;
            }


            return responseDto;
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
