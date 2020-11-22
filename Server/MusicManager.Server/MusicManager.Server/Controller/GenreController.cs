using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

namespace MusicManager.Server.Controller
{
    [Route("api/genre/")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponseDto>> Get()
        {
            var responseDto = new BaseResponseDto();

            var genres = await _genreRepository.GetAll();

            responseDto.Data.Add("genres", genres);

            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> Create(GenreRequestDto genreRequestDto)
        {
            var responseDto = new BaseResponseDto();

            GenreValidator validator = new GenreValidator();
            var validationResult = await validator.ValidateAsync(genreRequestDto);

            if(validationResult.IsValid)
            {
                var dbGenre = _genreRepository.GetByName(genreRequestDto.GenreName);

                if(dbGenre is null)
                {
                    var newGenre = new Genre
                    {
                        Name = genreRequestDto.GenreName
                    };

                    var newDbGenre = await _genreRepository.Insert(newGenre);

                    responseDto.Data.Add("genre", newDbGenre);
                }
                else
                {
                    responseDto.Infos.Errors.Add($"A Genre with the name {genreRequestDto.GenreName} exist already.");
                    responseDto.StatusCode = HttpStatusCode.UnprocessableEntity;
                }
            }
            else
            {
                responseDto.Infos.Errors.AddRange(validationResult.Errors.Select(error => error.ErrorMessage).ToList());
                responseDto.StatusCode = HttpStatusCode.UnprocessableEntity;
            }

            return StatusCode((int)responseDto.StatusCode, responseDto);
        }
    }
}
