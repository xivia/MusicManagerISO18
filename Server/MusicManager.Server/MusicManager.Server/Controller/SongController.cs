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
    [Authorize]
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
        public async Task<ActionResult<BaseResponseDto>> Create([FromForm(Name = "file")] IFormFile file, [FromForm] string songRequestDto)
        {
            BaseResponseDto responseDto = new BaseResponseDto();

            try
            {
                var requestDto = JsonConvert.DeserializeObject<SongRequestDto>(songRequestDto);
                responseDto = await _songService.Create(file, requestDto);
            }
            catch (Exception e)
            {
                responseDto.Infos.Errors.Add(e.Message);
                responseDto.StatusCode = HttpStatusCode.BadRequest;
            }

            return StatusCode((int)responseDto.StatusCode, responseDto);
        }
    }
}
