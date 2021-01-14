using MusicManager.Server.Core.Model;
using MusicManger.Server.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Repository
{
    public interface ISongRepository : IRepository<Song>
    {

    }

    public class SongRepository : GenericRepository<Song>, ISongRepository
    {
        public SongRepository(MusicManagerContext context) : base(context)
        {

        }
    }
}
