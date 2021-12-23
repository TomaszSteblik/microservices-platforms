using AutoMapper;
using CommandService;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.Profiles;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformPublishDto, Platform>()
            .ForMember(dest => dest.ExternalId, 
opt => opt.MapFrom(source => source.Id));
        CreateMap<GrpcPlatformModel, Platform>()
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(source => source.PlatformId))
            .ForMember(dest => dest.Name,
                opt =>
                    opt.MapFrom(source => source.Name))
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());
    }
}