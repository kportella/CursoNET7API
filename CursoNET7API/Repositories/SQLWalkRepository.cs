using CursoNET7API.Data;
using CursoNET7API.Models.Domain;

namespace CursoNET7API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {

        private readonly NZWalksDbContext dbcontext;

        public SQLWalkRepository(NZWalksDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }


        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbcontext.Walks.AddAsync(walk);
            await dbcontext.SaveChangesAsync();

            return walk;
        }
    }
}
