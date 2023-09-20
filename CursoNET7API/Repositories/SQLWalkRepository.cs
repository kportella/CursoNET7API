using CursoNET7API.Data;
using CursoNET7API.Models.Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await dbcontext.Walks.FindAsync(id);

            if (existingWalk == null) { return null; }
            dbcontext.Walks.Remove(existingWalk);
            await dbcontext.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true)
        {
            var walks = dbcontext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            if (!(string.IsNullOrWhiteSpace(filterOn) && string.IsNullOrWhiteSpace(filterQuery)))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            return await walks.ToListAsync();


            // return await dbcontext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbcontext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbcontext.Walks.FindAsync(id);
            if (existingWalk == null) { return null; }

            existingWalk.Name = walk.Name;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.Description = walk.Description;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;

            await dbcontext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
