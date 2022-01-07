using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using CommandsService.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;

[Route("api/c/platform/{platformId}/[controller]")]
[ApiController]
public class CommandController : ControllerBase
{
    private readonly ICommandRepo _commandRepo;
    private readonly IMapper _autoMapper;
    private readonly IMediator _mediator;
    
    public CommandController(IMapper autoMapper, ICommandRepo commandRepo, IMediator mediator)
    {
        _autoMapper = autoMapper;
        _commandRepo = commandRepo;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommandsForPlatform(int platformId)
    {
        var query = new GetCommandsForPlatformQuery(platformId);
        var result = await _mediator.Send(query);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("{commandId}", Name = nameof(GetCommandForPlatform))]
    public async Task<ActionResult<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
    {
        var query = new GetCommandForPlatformQuery(platformId, commandId);
        var result = await _mediator.Send(query);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CommandReadDto>> CreateCommand(int platformId, [FromBody] CommandCreateDto commandCreateDto)
    {
        if(!await _commandRepo.PlatformExists(platformId))
            return NotFound();
        
        var command = _autoMapper.Map<Command>(commandCreateDto);
        
        await _commandRepo.CreateCommand(platformId, command);
        var result = await _commandRepo.SaveChangesAsync();
        if (!result)
            return BadRequest();

        var commandReadDto = _autoMapper.Map<CommandReadDto>(command);

        return CreatedAtRoute(nameof(GetCommandForPlatform),
            new {platformId = platformId, commandId = commandReadDto.Id},
            commandReadDto);
    }
}