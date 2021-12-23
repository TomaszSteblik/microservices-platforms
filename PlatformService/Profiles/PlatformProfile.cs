using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();
        CreateMap<PlatformReadDto, PlatformPublishDto>();
        CreateMap<Platform, GrpcPlatformModel>()
        .ForMember(dest => dest.PlatformId, 
    opt => opt.MapFrom(s=>s.Id))
        .ForMember(dest => dest.Name, 
    opt => opt.MapFrom(s=>s.Name))
        .ForMember(dest => dest.Publisher, 
    opt => opt.MapFrom(s=>s.Publisher));
    }
}