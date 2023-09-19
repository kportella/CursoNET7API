using AutoMapper;
using CursoNET7API.Models.Domain;
using CursoNET7API.Models.DTO;

namespace CursoNET7API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionRequestDto>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();

        }
    }
}
