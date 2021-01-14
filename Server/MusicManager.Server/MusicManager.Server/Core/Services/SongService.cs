﻿using Microsoft.AspNetCore.Http;
using MusicManager.Server.Core.DataTransferObjects;
using MusicManager.Server.Core.DataTransferObjects.Mapper;
using MusicManager.Server.Core.DataTransferObjects.SongDtos;
using MusicManager.Server.Core.Model;
using MusicManager.Server.Core.Repository;
using MusicManager.Server.Core.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Services
{
    public interface ISongService
    {
        Task<BaseResponseDto> Create(IFormFile formFile, SongRequestDto songRequestDto);
    }

    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IFileService _fileService;
        private readonly IRequestDataService _requestDataService;
        private readonly string UPLOAD_DIRECTORY = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\\Public\\Music";

        public SongService(IFileService fileService, 
            ISongRepository songRepository, 
            IGenreRepository genreRepository,
            IRequestDataService requestDataService)
        {
            _fileService = fileService;
            _songRepository = songRepository;
            _genreRepository = genreRepository;
            _requestDataService = requestDataService;
        }

        public async Task<BaseResponseDto> Create(IFormFile file, SongRequestDto songRequestDto)
        {
            var response = new BaseResponseDto();

            try
            {
                var dbGenre = await _genreRepository.GetById(songRequestDto.GenreId);

                if(dbGenre is null)
                {
                    response.StatusCode = HttpStatusCode.UnprocessableEntity;
                    response.Infos.Errors.Add($"Genre with id {songRequestDto.GenreId} has not been found");
                    return response;
                }
                 
                var validator = new SongRequestDtoValidator();
                var validationResult = validator.Validate(songRequestDto);

                if(!validationResult.IsValid)
                {
                    response.StatusCode = HttpStatusCode.UnprocessableEntity;
                    response.Infos.Errors.AddRange(validationResult.Errors.Select(error => error.ErrorMessage));
                    return response;
                }

                var fileUploadResponse = await _fileService.UploadFile(file, UPLOAD_DIRECTORY, new SongFileValidator());

                if(fileUploadResponse.HasError)
                {
                    response.StatusCode = HttpStatusCode.UnprocessableEntity;
                    response.Infos.Errors.AddRange(fileUploadResponse.Infos.Errors);
                    return response;
                }

                var user = await _requestDataService.GetCurrentUser();

                var dbNewSong = new Song
                {
                    Artist = user,
                    FilePath = fileUploadResponse.FilePath,
                    Name = songRequestDto.Name,
                    PublishOn = songRequestDto.PublishOn,
                    Deleted = false,
                    SongGenre = dbGenre
                };

                var newSong = await _songRepository.Insert(dbNewSong);

                response.Data.Add("song", SongResponseDtoMapper.DbToDto(newSong));
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