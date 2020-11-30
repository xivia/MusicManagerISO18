using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicManager.Server.Core.DataTransferObjects;
using MusicManager.Server.Core.DataTransferObjects.UserDtos;
using MusicManager.Server.Core.Services;

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
        public async Task<ActionResult<BaseResponseDto>> Login([FromBody] UserLoginDto user)
        {
            var responseDto = await _userService.Login(user);
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

        [Authorize]
        [HttpPut("{userId}/ban")]
        public async Task<ActionResult<BaseResponseDto>> Ban(long userId) 
        {
            var responseDto = await _userService.BanOrUnbanUser(userId, true);
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

        [Authorize]
        [HttpPut("{userId}/unban")]
        public async Task<ActionResult<BaseResponseDto>> Unban(long userId)
        {
            var responseDto = await _userService.BanOrUnbanUser(userId, false);
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }
    }
}
