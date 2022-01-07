using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Queries;
using MediatR;

namespace CommandsService.Handlers;

public class GetCommandsForPlatformHandler : IRequestHandler<GetCommandsForPlatformQuery, IEnumerable<CommandReadDto>>
{
    private readonly ICommandRepo _commandRepo;
    private readonly IMapper _mapper;
    
    public GetCommandsForPlatformHandler(ICommandRepo commandRepo, IMapper mapper)
    {
        _commandRepo = commandRepo;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<CommandReadDto>> Handle(GetCommandsForPlatformQuery request, CancellationToken cancellationToken)
    {
        if(!await _commandRepo.PlatformExists(request.Id))
            return null;

        var commands = await _commandRepo.GetCommandsForPlatform(request.Id);
        
        return _mapper.Map<IEnumerable<CommandReadDto>>(commands);
    }
}