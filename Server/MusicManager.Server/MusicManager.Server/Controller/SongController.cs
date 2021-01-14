using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicManager.Server.Core.DataTransferObjects;
using MusicManager.Server.Core.DataTransferObjects.SongDtos;
using MusicManager.Server.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Controller
{
    [Route("api/music/")]
    [ApiController]
    [Authorize]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        public async Task<ActionResult<BaseResponseDto>> Create(SongRequestDto songRequestDto)
        {
            var responseDto = await _songService.Create(songRequestDto);
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }
    }
}
