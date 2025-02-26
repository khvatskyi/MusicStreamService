using MusicStreamServiceApp.DAL.EFCoreContexts;
using MusicStreamServiceApp.DAL.Entities;
using MusicStreamServiceApp.DAL.Interfaces.IEntityRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStreamServiceApp.DAL.Repositories.EntityRepositories
{
    public class GenreRepository : GenericRepository<Genre, int>, IGenreRepository
    {
        public GenreRepository(MusicDBContext db)
            : base(db)
        {
        }

        public async Task<Genre> GetByName(string Name)
        {
            return await db.Set<Genre>().FirstOrDefaultAsync(e => e.Name == Name);
        }
    }
}
