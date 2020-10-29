using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicManager.Server.Core.DataTransferObjects;
using MusicManager.Server.Core.DataTransferObjects.Mapper;
using MusicManager.Server.Core.Model;
using MusicManager.Server.Core.Repository;

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

            responseDto.Data.Add("user", UserResponseDtoMapper.FromDb(user));

            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

    }
}
