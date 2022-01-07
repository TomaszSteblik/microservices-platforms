using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Commands;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.Queries;
using PlatformService.SyncDataServices;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformController : ControllerBase
{

    private readonly IMediator _mediator;

    public PlatformController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms()
    {
        var query = new GetAllPlatformsQuery();
        var result = _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id}",Name = "GetPlatformById")]
    public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
    {
        var query = new GetPlatformByIdQuery(id);
        var result = _mediator.Send(query);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform([FromBody] PlatformCreateDto platformCreateDto)
    {
        var command = new CreatePlatformCommand(platformCreateDto);
        var result = await _mediator.Send(command);
        return CreatedAtRoute(nameof(GetPlatformById), new {Id = result.Id}, result);
    }
}