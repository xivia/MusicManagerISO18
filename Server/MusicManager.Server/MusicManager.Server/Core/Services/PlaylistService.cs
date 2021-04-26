﻿using MusicManager.Server.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicManager.Server.Core.Model;
using MusicManager.Server.Core.Repository;
using MusicManager.Server.Core.DataTransferObjects.PlaylistDtos;
using MusicManager.Server.Core.DataTransferObjects.Mapper;
using System.Net;

namespace MusicManager.Server.Core.Services
{
    public interface IPlaylistService
    {
        Task<BaseResponseDto> Create(PlaylistDto playlistDto);
        Task<BaseResponseDto> GetById(long songId);
        Task<BaseResponseDto> DeleteById(long songId);
        Task<BaseResponseDto> UpdateById(long songId);
    }

    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IRequestDataService _requestDataService;

        public PlaylistService(IPlaylistRepository playlistRepository, IRequestDataService requestDataService)
        {
            _playlistRepository = playlistRepository;
            _requestDataService = requestDataService;
        }
        public async Task<BaseResponseDto> Create(PlaylistDto playlistDto)
        {
            var responseDto = new BaseResponseDto();

            try
            {
                var newDbPlaylist = await _playlistRepository.Insert(PlaylistMapper.DtoToDb(playlistDto));

                responseDto.AddInfo("Created Playlist successful");
                responseDto.StatusCode = HttpStatusCode.OK;

                return responseDto;
            }
            catch (Exception ex)
            {
                responseDto.AddError(ex.Message);
                responseDto.StatusCode = HttpStatusCode.InternalServerError;

            }
            return responseDto;


        }

        public async Task<BaseResponseDto> DeleteById(long id)
        {
            var response = new BaseResponseDto();

            try
            {
                var dbPlaylist = await _playlistRepository.GetById(id);

                if (dbPlaylist is null)
                {
                    response.Infos.Errors.Add($"Playlist with id {id} has not been found");
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var currentUser = await _requestDataService.GetCurrentUser();

                if (dbPlaylist.UserId != currentUser.UserId)
                {
                    response.Infos.Errors.Add($"Song with id {id} cannot be deleted because it isn't linked to your account");
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    return response;
                }
                await _playlistRepository.Delete(dbPlaylist);

            }
            catch (Exception e)
            {
                response.Infos.Errors.Add(e.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<BaseResponseDto> GetById(long id)
        {
            var response = new BaseResponseDto();

            try
            {
                var dbPlaylist = await _playlistRepository.GetById(id);

                if (dbPlaylist is null)
                {
                    response.Infos.Errors.Add($"Playlist with id {id} has not been found");
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                response.Data.Add("song", PlaylistMapper.DbToDto(dbPlaylist));
            }
            catch (Exception e)
            {
                response.Infos.Errors.Add(e.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<BaseResponseDto> UpdateById(long id)
        {
            var response = new BaseResponseDto();

            try
            {
                var dbPlaylist = await _playlistRepository.GetById(id);

                if (dbPlaylist is null)
                {
                    response.Infos.Errors.Add($"Playlist with id {id} has not been found");
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var currentUser = await _requestDataService.GetCurrentUser();

                if (dbPlaylist.UserId != currentUser.UserId)
                {
                    response.Infos.Errors.Add($"Song with id {id} cannot be deleted because it isn't linked to your account");
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    return response;
                }
                await _playlistRepository.Update(dbPlaylist);

            }
            catch (Exception e)
            {
                response.Infos.Errors.Add(e.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}