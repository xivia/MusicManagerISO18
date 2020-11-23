using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
using MusicManager.Server.Core.Services;
using MusicManager.Server.Core.Validators;

namespace MusicManager.Server.Controller
{
    [Route("api/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> Get()
        {
            var responseDto = await _userService.GetAll();
            return StatusCode((int) responseDto.StatusCode, responseDto);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult<BaseResponseDto>> GetById(long userId)
        {
            var responseDto = await _userService.GetById(userId);
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponseDto>> Create([FromBody] UserDto userDto)
        {
            var responseDto = await _userService.Create(userDto);
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponseDto>> Login([FromBody] UserDto user)
        {
            var responseDto = await _userService.Login(user);

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
