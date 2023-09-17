using CursoNET7API.Models.Domain;

namespace CursoNET7API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
    }
}
