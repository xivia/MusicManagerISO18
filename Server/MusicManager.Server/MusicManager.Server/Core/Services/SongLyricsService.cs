using MusicManager.Server.Core.DataTransferObjects;
using MusicManager.Server.Core.DataTransferObjects.Mapper;
using MusicManager.Server.Core.DataTransferObjects.SongLyricsDtos;
using MusicManager.Server.Core.Model;
using MusicManager.Server.Core.Repository;
using MusicManager.Server.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Services
{
    public interface ISongLyricsService
    {
        Task<BaseResponseDto> GetSongLyricsBySongId(long songId);
        Task<BaseResponseDto> CreateForSong(SongLyricsRequestDto songLyricsRequestDto);
    }

    public class SongLyricsService : ISongLyricsService
    {
        private readonly ISongLyricsRepository _songLyricsRepository;
        private readonly ISongRepository _songRepository;
        private readonly IRequestDataService _requestDataService;

        public SongLyricsService(ISongLyricsRepository songLyricsRepository, ISongRepository songRepository, IRequestDataService requestDataService)
        {
            _songLyricsRepository = songLyricsRepository;
            _songRepository = songRepository;
            _requestDataService = requestDataService;
        }

        public async Task<BaseResponseDto> GetSongLyricsBySongId(long songId)
        {
            var response = new BaseResponseDto();

            try
            {
                var dbSong = await _songRepository.GetById(songId);

                if(dbSong is null)
                {
                    response.AddError($"Song with id {songId} has not been found");
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var dbLyrics = await _songLyricsRepository.GetBySongId(songId);

                if(dbLyrics is null)
                {
                    response.AddError($"No lyrics found for song with id {songId}");
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                response.Data.Add("lyrics", SongLyricsMapper.DbToDto(dbLyrics));
            }
            catch (Exception e)
            {
                response.AddError(e.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<BaseResponseDto> CreateForSong(SongLyricsRequestDto songLyricsRequestDto)
        {
            var response = new BaseResponseDto();

            try
            {
                var dbSong = await _songRepository.GetById(songLyricsRequestDto.SongId);

                if (dbSong is null)
                {
                    response.AddError($"Song with id {songLyricsRequestDto.SongId} has not been found");
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var currentUser = await _requestDataService.GetCurrentUser();

                if(dbSong.Artist.UserId != currentUser.UserId)
                {
                    response.AddError("You can only add lyrics to your own songs");
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    return response;
                }

                var dbExistingLyrics = await _songLyricsRepository.GetBySongId(songLyricsRequestDto.SongId);

                if(dbExistingLyrics != null)
                {
                    response.AddError($"There are already lyrics for the song with the id {songLyricsRequestDto.SongId}. Please update them instead of adding new ones");
                    response.StatusCode = HttpStatusCode.Conflict;
                    return response;
                }

                var songLyricsRequestDtoValidator = new SongLyricsRequestDtoValidator();
                var validationResult = await songLyricsRequestDtoValidator.ValidateAsync(songLyricsRequestDto);

                if (!validationResult.IsValid)
                {
                    response.AddErrors(validationResult.Errors.Select(error => error.ErrorMessage).ToList());
                    response.StatusCode = HttpStatusCode.UnprocessableEntity;
                    return response;
                }

                var newDbSongLyrics = new SongLyrics
                {
                    Lyrics = songLyricsRequestDto.SongLyrics,
                    Song = dbSong                       
                };

                await _songLyricsRepository.Insert(newDbSongLyrics);

                response.Data.Add("lyrics", SongLyricsMapper.DbToDto(newDbSongLyrics));
            }
            catch (Exception e)
            {
                response.AddError(e.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

    }
}
