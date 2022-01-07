using AutoMapper;
using MediatR;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Queries;

namespace PlatformService.Handlers;

public class GetPlatformByIdHandler : IRequestHandler<GetPlatformByIdQuery, PlatformReadDto>
{
    private readonly IPlatformRepo _platformRepo;
    private readonly IMapper _mapper;

    public GetPlatformByIdHandler(IPlatformRepo platformRepo, IMapper mapper)
    {
        _platformRepo = platformRepo;
        _mapper = mapper;
    }
    
    public async Task<PlatformReadDto> Handle(GetPlatformByIdQuery request, CancellationToken cancellationToken)
    {
        var platform = await _platformRepo.GetPlatformById(request.Id);

        return platform is null ? null : _mapper.Map<PlatformReadDto>(platform);
    }
}