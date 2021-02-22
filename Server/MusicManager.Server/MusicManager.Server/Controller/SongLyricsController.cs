using Microsoft.AspNetCore.Mvc;
using MusicManager.Server.Core.DataTransferObjects.SongLyricsDtos;
using MusicManager.Server.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Controller
{
    [Route("api/lyrics/")]
    [ApiController]
    public class SongLyricsController : ControllerBase
    {
        private readonly ISongLyricsService _songLyricsService;

        public SongLyricsController(ISongLyricsService songLyricsService)
        {
            _songLyricsService = songLyricsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateForSong(SongLyricsRequestDto songLyricsRequestDto)
        {
            var responseDto = await _songLyricsService.CreateForSong(songLyricsRequestDto);
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

        [HttpGet("song/{songId}")]
        public async Task<IActionResult> GetBySongId(long songId)
        {
            var responseDto = await _songLyricsService.GetSongLyricsBySongId(songId);
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

    }
}
