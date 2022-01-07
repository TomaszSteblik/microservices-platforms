using AutoMapper;
using MediatR;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.Queries;

namespace PlatformService.Handlers;

public class GetAllPlatformsHandler : IRequestHandler<GetAllPlatformsQuery, IEnumerable<PlatformReadDto>>
{
    private readonly IPlatformRepo _platformRepo;
    private readonly IMapper _mapper;
    
    public GetAllPlatformsHandler(IPlatformRepo platformRepo, IMapper mapper)
    {
        _platformRepo = platformRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PlatformReadDto>> Handle(GetAllPlatformsQuery request, CancellationToken cancellationToken)
    {
        var platforms = await _platformRepo.getAllPlatforms();
        return _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
    }
}