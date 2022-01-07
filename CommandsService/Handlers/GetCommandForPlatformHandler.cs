using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Queries;
using MediatR;

namespace CommandsService.Handlers;

public class GetCommandForPlatformHandler : IRequestHandler<GetCommandForPlatformQuery, CommandReadDto>
{
    private readonly IMapper _mapper;
    private readonly ICommandRepo _commandRepo;
    
    public GetCommandForPlatformHandler(IMapper mapper, ICommandRepo commandRepo)
    {
        _mapper = mapper;
        _commandRepo = commandRepo;
    }
    
    public async Task<CommandReadDto> Handle(GetCommandForPlatformQuery request, CancellationToken cancellationToken)
    {
        if (!await _commandRepo.PlatformExists(request.PlatformId))
            return null;
        
        var command = await _commandRepo.GetCommand(request.PlatformId, request.CommandId);

        return command is null ? null : _mapper.Map<CommandReadDto>(command);
    }
}