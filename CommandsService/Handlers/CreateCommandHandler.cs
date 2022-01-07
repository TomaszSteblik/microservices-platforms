using AutoMapper;
using CommandsService.Commands;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using MediatR;

namespace CommandsService.Handlers;

public class CreateCommandHandler : IRequestHandler<CreateCommandCommand, CommandReadDto>
{
    private readonly ICommandRepo _commandRepo;
    private readonly IMapper _mapper;
    
    public CreateCommandHandler(ICommandRepo commandRepo, IMapper mapper)
    {
        _commandRepo = commandRepo;
        _mapper = mapper;
    }
    
    public async Task<CommandReadDto> Handle(CreateCommandCommand request, CancellationToken cancellationToken)
    {
        if(!await _commandRepo.PlatformExists(request.PlatformId))
            return null;
        
        var command = _mapper.Map<Command>(request.CommandCreateDto);
        
        await _commandRepo.CreateCommand(request.PlatformId, command);
        await _commandRepo.SaveChangesAsync();
        
        return _mapper.Map<CommandReadDto>(command);
    }
}