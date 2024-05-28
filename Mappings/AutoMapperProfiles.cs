using AutoMapper;
using NzWalksAPI.Models.DTO;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, RegionDtoV2>().ReverseMap();
            CreateMap<Region, AddRegionRequestDto>().ReverseMap();

            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Walk, WalkDtoV2>().ReverseMap();
            CreateMap<Walk, AddWalkRequestDto>().ReverseMap();

            CreateMap<Difficulty, DifficultyDto>().ReverseMap();

            CreateMap<Image, ImageUploadRequstDto>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
        }
    }
}