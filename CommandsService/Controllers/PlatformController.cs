using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using CommandsService.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public class PlatformController : ControllerBase
{

    private readonly IMediator _mediator;
    public PlatformController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> TestInBoundConnection()
    {
        Console.WriteLine("--> Inbound POST # Command Service");
        return Ok("Inbound test of from Platforms Controller");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetAllPlatforms()
    {
        var query = new GetAllPlatformsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}