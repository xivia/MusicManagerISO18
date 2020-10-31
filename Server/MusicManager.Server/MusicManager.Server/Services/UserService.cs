using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicManager.Server.Core.Config;
using MusicManager.Server.Core.DataTransferObjects;
using MusicManager.Server.Core.DataTransferObjects.Mapper;
using MusicManager.Server.Core.DataTransferObjects.UserDtos;
using MusicManager.Server.Core.Model;
using MusicManager.Server.Core.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MusicManager.Server.Services
{
    public interface IUserService
    {
        Task<BaseResponseDto> Authenticate(UserDto model);
        Task<User> GetById(long userId);
        string GenerateJwtToken(User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public async Task<BaseResponseDto> Authenticate(UserDto model)
        {
            var user = await _userRepository.GetByUsername(model.UserName);

            // return null if user not found
            if (user == null) return null;

            if (user.Password != model.Password) return null; // Fuck it no encryption :D

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            var response = new BaseResponseDto();

            response.Data.Add("User", UserResponseDtoMapper.FromDb(user));
            response.Data.Add("Token", token);

            return response;
        }

        // helper methods

        public string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
           
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> GetById(long userId)
        {
            return await _userRepository.GetById(userId);
        }
    }
}
