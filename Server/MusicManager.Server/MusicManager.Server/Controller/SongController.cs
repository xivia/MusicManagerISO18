using Microsoft.AspNetCore.Mvc;
using MusicManager.Server.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Controller
{
    [Route("api/music/")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongRepository _songRepository;

        public SongController(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

    }
}
