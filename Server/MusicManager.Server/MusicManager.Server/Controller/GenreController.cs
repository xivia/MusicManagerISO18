using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicManager.Server.Core.DataTransferObjects;
using MusicManager.Server.Core.DataTransferObjects.GenreDtos;
using MusicManager.Server.Core.Model;
using MusicManager.Server.Core.Repository;
using MusicManager.Server.Core.Services;
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
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponseDto>> Get()
        {
            var responseDto = await _genreService.GetAll();
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> Create(GenreRequestDto genreRequestDto)
        {
            var responseDto = await _genreService.Create(genreRequestDto);
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }
    }
}
