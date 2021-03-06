﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicManager.Server.Core.DataTransferObjects;
using MusicManager.Server.Core.DataTransferObjects.PlaylistDtos;
using MusicManager.Server.Core.Services;
using System.Threading.Tasks;

namespace MusicManager.Server.Controller
{
    [Route("api/playlist/")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpGet("{playlistId}/shuffle")]
        public async Task<ActionResult<BaseResponseDto>> ShufflePlaylist(long playlistId)
        {
            var response = await _playlistService.ShufflePlaylistById(playlistId);
            return response;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> Create(PlaylistDto playlistDto)
        {
            var response = await _playlistService.Create(playlistDto);
            return response;
        }

        [HttpDelete("{playlistId}")]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> Delete(long playlistId)
        {
            var response = await _playlistService.DeleteById(playlistId);
            return response;
        }

        [HttpGet("{playlistId}")]
        public async Task<ActionResult<BaseResponseDto>> GetById(long playlistId)
        {
            var response = await _playlistService.GetById(playlistId);
            return response;
        }

        [HttpPut("{playlistId}")]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> Update(PlaylistDto playlistDto)
        {
            var response = await _playlistService.Update(playlistDto);
            return response;
        }

        [HttpPut("{playlistId}/add/{songId}")]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> AddSongToPlaylist(long playlistId, long songId)
        {
            var response = await _playlistService.AddSongToPlaylist(playlistId, songId);
            return response;
        }

    }
}
