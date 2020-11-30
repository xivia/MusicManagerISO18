using Microsoft.EntityFrameworkCore;
using MusicManager.Server.Core.Model;
using MusicManger.Server.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Repository
{
    public interface IPasswordResetLinkRepository : IRepository<PasswordResetLink>
    {
        Task<PasswordResetLink> GetResetLinkByLinkGuid(Guid linkId);
    }

    public class PasswordResetLinkRepository : GenericRepository<PasswordResetLink>, IPasswordResetLinkRepository
    {
        public PasswordResetLinkRepository(MusicManagerContext context) : base(context)
        {

        }

        public async Task<PasswordResetLink> GetResetLinkByLinkGuid(Guid linkGuid)
        {
            return await _context.PasswordResetLinks
                            .Where(resetLink => resetLink.LinkGuid == linkGuid)
                            .Include(resetLink => resetLink.User)
                            .FirstOrDefaultAsync();
        }
    }
}
