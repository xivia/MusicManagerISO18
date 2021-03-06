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
using MoreLinq;

namespace MusicManager.Server.Core.Services
{
    public interface IPlaylistService
    {
        Task<BaseResponseDto> Create(PlaylistDto playlistDto);
        Task<BaseResponseDto> GetById(long songId);
        Task<BaseResponseDto> ShufflePlaylistById(long songId);
        Task<BaseResponseDto> DeleteById(long songId);
        Task<BaseResponseDto> Update(PlaylistDto dto);
        Task<BaseResponseDto> AddSongToPlaylist(long playlistId, long songId);
    }

    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IRequestDataService _requestDataService;
        private readonly ISongRepository _songRepository;

        public PlaylistService(IPlaylistRepository playlistRepository, IRequestDataService requestDataService, ISongRepository songRepository)
        {
            _playlistRepository = playlistRepository;
            _requestDataService = requestDataService;
            _songRepository = songRepository;
        }
        public async Task<BaseResponseDto> Create(PlaylistDto playlistDto)
        {
            var responseDto = new BaseResponseDto();

            try
            {
                List<Song> songs = await _songRepository.GetBySongIds(playlistDto.SongIds);

                if(songs.Count < 1)
                {
                    responseDto.StatusCode = HttpStatusCode.NotFound;
                    responseDto.AddError("No songs found");
                    return responseDto;
                }

                playlistDto.Songs = songs;

                User user = await _requestDataService.GetCurrentUser();

                playlistDto.User = user;

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

                response.Data.Add("playlist", PlaylistMapper.DbToDto(dbPlaylist));
            }
            catch (Exception e)
            {
                response.Infos.Errors.Add(e.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<BaseResponseDto> Update(PlaylistDto dto)
        {
            var response = new BaseResponseDto();

            try
            {
                if (dto is null)
                {
                    response.Infos.Errors.Add($"Playlist with id {dto.PlaylistId} has not been found");
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var currentUser = await _requestDataService.GetCurrentUser();

                await _playlistRepository.Update(PlaylistMapper.DtoToDb(dto));

            }
            catch (Exception e)
            {
                response.Infos.Errors.Add(e.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<BaseResponseDto> ShufflePlaylistById(long playlistId)
        {
            var response = new BaseResponseDto();

            try
            {
                var playlist = await _playlistRepository.GetById(playlistId);

                if(playlist is null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.AddError($"Playlist with id {playlistId} not found");
                }

                playlist.Songs = playlist.Songs.Shuffle().ToList();

                response.Data.Add("playlist", PlaylistMapper.DbToDto(playlist));
            }
            catch (Exception e)
            {
                response.Infos.Errors.Add(e.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<BaseResponseDto> AddSongToPlaylist(long playlistId, long songId)
        {
            var responseDto = new BaseResponseDto();

            try
            {
                var playlist = await _playlistRepository.GetById(playlistId);

                if(playlist is null)
                {
                    responseDto.StatusCode = HttpStatusCode.NotFound;
                    responseDto.AddError($"Playlist with id {playlistId} not found");
                    return responseDto;
                }

                var song = await _songRepository.GetById(songId);

                if(song is null)
                {
                    responseDto.StatusCode = HttpStatusCode.NotFound;
                    responseDto.AddError($"Song with id {songId} not found");
                    return responseDto;
                }

                var user = await _requestDataService.GetCurrentUser();

                if(playlist.User != user)
                {
                    responseDto.StatusCode = HttpStatusCode.Unauthorized;
                    return responseDto;
                }

                playlist.Songs.Add(song);

                await _playlistRepository.Update(playlist);
            }
            catch (Exception e)
            {
                responseDto.Infos.Errors.Add(e.Message);
                responseDto.StatusCode = HttpStatusCode.InternalServerError;
            }

            return responseDto;
        }

    }
}
