using AutoMapper;
using CommandsService.Commands;
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
    private readonly IMediator _mediator;
    
    public CommandController(IMediator mediator)
    {
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
        var command = new CreateCommandCommand(commandCreateDto, platformId);
        var result = await _mediator.Send(command);
        return CreatedAtRoute(nameof(GetCommandForPlatform),
            new {platformId = result.PlatformId, commandId = result.Id}, result);
    }
}