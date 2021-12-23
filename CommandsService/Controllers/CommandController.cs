using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;

[Route("api/c/platform/{platformId}/[controller]")]
[ApiController]
public class CommandController : ControllerBase
{
    private readonly ICommandRepo _commandRepo;
    private readonly IMapper _autoMapper;
    
    public CommandController(IMapper autoMapper, ICommandRepo commandRepo)
    {
        _autoMapper = autoMapper;
        _commandRepo = commandRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommandsForPlatform(int platformId)
    {
        if(!await _commandRepo.PlatformExists(platformId))
            return NotFound();
        
        return Ok(_autoMapper.Map<IEnumerable<CommandReadDto>>(await _commandRepo.GetCommandsForPlatform(platformId)));
    }

    [HttpGet("{commandId}", Name = nameof(GetCommandForPlatform))]
    public async Task<ActionResult<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
    {
        if(!await _commandRepo.PlatformExists(platformId))
            return NotFound();
        
        var command = await _commandRepo.GetCommand(platformId, commandId);

        if (command is null)
            return NotFound();
        
        return Ok(_autoMapper.Map<CommandReadDto>(command));
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