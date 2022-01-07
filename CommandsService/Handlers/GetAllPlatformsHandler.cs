using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Queries;
using MediatR;

namespace CommandsService.Handlers;

public class GetAllPlatformsHandler : IRequestHandler<GetAllPlatformsQuery, IEnumerable<PlatformReadDto>>
{
    private readonly IMapper _mapper;
    private readonly ICommandRepo _commandRepo;
    
    public GetAllPlatformsHandler(IMapper mapper, ICommandRepo commandRepo)
    {
        _mapper = mapper;
        _commandRepo = commandRepo;
    }
    
    public async Task<IEnumerable<PlatformReadDto>> Handle(GetAllPlatformsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<PlatformReadDto>>(await _commandRepo.GetAllPlatforms());
    }
}