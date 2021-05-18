using MusicStreamServiceApp.DAL.EFCoreContexts;
using MusicStreamServiceApp.DAL.Entities;
using MusicStreamServiceApp.DAL.Interfaces.IEntityRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStreamServiceApp.DAL.Repositories.EntityRepositories
{
    public class MusicGenreRepository : GenericRepository<MusicGenre, int>, IMusicGenreRepository
    {
        public MusicGenreRepository(MusicDBContext db)
            : base(db)
        {
        }

        public async Task<ICollection<MusicGenre>> GetByMusicId(int MusicId)
        {
            return await db.Set<MusicGenre>().Where(e => e.MusicId == MusicId).ToListAsync();
        }
    }
}
