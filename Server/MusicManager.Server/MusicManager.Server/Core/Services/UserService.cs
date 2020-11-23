﻿using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MusicManager.Server.Core.DataTransferObjects;
using MusicManager.Server.Core.DataTransferObjects.Mapper;
using MusicManager.Server.Core.DataTransferObjects.UserDtos;
using MusicManager.Server.Core.Model;
using MusicManager.Server.Core.Repository;
using MusicManager.Server.Core.Validators;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Services
{
    public interface IUserService
    {
        Task<BaseResponseDto> GetAll();
        Task<BaseResponseDto> GetById(long userId);
        Task<BaseResponseDto> Create(UserDto userDto);
        Task<BaseResponseDto> Login(UserDto user);
    }

    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IConfiguration _config;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _config = configuration;
        }

        public async Task<BaseResponseDto> GetAll()
        {
            var responseDto = new BaseResponseDto();

            List<User> users = await _userRepository.GetAll();

            responseDto.Data.Add("users", UserResponseDtoMapper.FromDb(users));

            return responseDto;
        }

        public async Task<BaseResponseDto> GetById(long userId)
        {
            var responseDto = new BaseResponseDto();

            User user = await _userRepository.GetById(userId);

            if (user is null) responseDto.StatusCode = HttpStatusCode.NotFound;

            responseDto.Data.Add("user", UserResponseDtoMapper.FromDb(user));

            return responseDto;
        }

        public async Task<BaseResponseDto> Create(UserDto userDto)
        {
            var responseDto = new BaseResponseDto();

            UserDtoValidator userDtoValidator = new UserDtoValidator();
            ValidationResult validationResult = userDtoValidator.Validate(userDto);

            if (validationResult.IsValid)
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

                    responseDto.Infos.Messages.Add("Successfully created user.");
                    responseDto.Data.Add("user", userResponseDto);
                    responseDto.Data.Add("token", GenerateJSONWebToken(user));
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

            return responseDto;
        }

        public async Task<BaseResponseDto> Login(UserDto user)
        {
            var responseDto = new BaseResponseDto();
            responseDto.StatusCode = HttpStatusCode.Unauthorized;

            var dbUser = await _userRepository.GetByUsername(user.UserName);

            if (dbUser is null) return responseDto;

            if (dbUser.Password != user.Password) return responseDto;

            if (dbUser.Banned)
            {
                responseDto.Infos.Errors.Add("You are banned!");
                return responseDto;
            }

            var token = GenerateJSONWebToken(dbUser);
            responseDto.Data.Add("Token", token);
            responseDto.StatusCode = HttpStatusCode.OK;

            return responseDto;
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.Name)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
