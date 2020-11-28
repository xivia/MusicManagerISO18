using MusicManager.Server.Core.DataTransferObjects;
using MusicManager.Server.Core.DataTransferObjects.GenreDtos;
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
    public interface IGenreService
    {
        Task<BaseResponseDto> GetAll();
        Task<BaseResponseDto> Create(GenreRequestDto genreRequestDto);
    }

    public class GenreService : IGenreService
    {

        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<BaseResponseDto> GetAll()
        {
            var responseDto = new BaseResponseDto();

            var genres = await _genreRepository.GetAll();

            responseDto.Data.Add("genres", genres);

            return responseDto;
        }

        public async Task<BaseResponseDto> Create(GenreRequestDto genreRequestDto)
        {
            var responseDto = new BaseResponseDto();

            GenreValidator validator = new GenreValidator();
            var validationResult = await validator.ValidateAsync(genreRequestDto);

            if(!validationResult.IsValid)
            {
                responseDto.Infos.Errors.AddRange(validationResult.Errors.Select(error => error.ErrorMessage));
                responseDto.StatusCode = HttpStatusCode.UnprocessableEntity;
                return responseDto;
            }

            var dbGenre = _genreRepository.GetByName(genreRequestDto.GenreName);

            if (dbGenre != null)
            {
                responseDto.Infos.Errors.Add($"A Genre with the name {genreRequestDto.GenreName} exist already.");
                responseDto.StatusCode = HttpStatusCode.UnprocessableEntity;
                return responseDto;
            }

            var newGenre = new Genre
            {
                Name = genreRequestDto.GenreName,
            };

            var newDbGenre = await _genreRepository.Insert(newGenre);

            responseDto.Data.Add("genre", newDbGenre);

            return responseDto;
        }

    }
}
