using CursoNET7API.Data;
using CursoNET7API.Models.Domain;
using CursoNET7API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CursoNET7API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }
    }
}
