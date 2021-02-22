using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicManager.Server.Core.DataTransferObjects;
using MusicManager.Server.Core.DataTransferObjects.SongDtos;
using MusicManager.Server.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MusicManager.Server.Controller
{
    [Route("api/song/")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> Create([FromForm(Name = "file")] IFormFile file, [FromForm(Name = "coverFile")] IFormFile coverFile, [FromForm] string songRequestDto)
        {
            BaseResponseDto responseDto = new BaseResponseDto();

            try
            {
                var requestDto = JsonConvert.DeserializeObject<SongRequestDto>(songRequestDto);
                responseDto = await _songService.Create(file, coverFile, requestDto);
            }
            catch (Exception e)
            {
                responseDto.Infos.Errors.Add(e.Message);
                responseDto.StatusCode = HttpStatusCode.BadRequest;
            }

            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

        [HttpGet("{songId}/stream")]
        public async Task<IActionResult> GetStreamById(long songId)
        {
            var response = await _songService.GetFilePathBySongId(songId);

            if(response.HasError)
            {
                return StatusCode((int) response.StatusCode);
            }

            var fileResponse = File(System.IO.File.OpenRead(response.FilePath), "audio/mpeg");
            fileResponse.EnableRangeProcessing = true;

            return fileResponse;
        }

        [HttpGet("{songId}")]
        public async Task<IActionResult> GetById(long songId)
        {
            var responseDto = await _songService.GetById(songId);
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

        [HttpDelete("{songId}")]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> DeleteById(long songId)
        {
            var responseDto = await _songService.DeleteById(songId);
            return StatusCode((int)responseDto.StatusCode, responseDto);
        }

    }
}
