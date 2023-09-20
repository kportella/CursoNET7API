using CursoNET7API.Models.Domain;

namespace CursoNET7API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk?> GetByIdAsync(Guid id);
    }
}
