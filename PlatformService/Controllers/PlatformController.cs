using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
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
    private readonly IPlatformRepo _platformRepo;
    private readonly IMapper _mapper;
    private readonly ICommandDataClient _commandDataClient;
    private readonly IMessageBusClient _messageBusClient;
    private readonly IMediator _mediator;

    public PlatformController(IPlatformRepo platformRepo, IMapper mapper, ICommandDataClient commandDataClient, IMessageBusClient messageBusClient, IMediator mediator)
    {
        _platformRepo = platformRepo;
        _mapper = mapper;
        _commandDataClient = commandDataClient;
        _messageBusClient = messageBusClient;
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
        var platform = _mapper.Map<Platform>(platformCreateDto);

        await _platformRepo.CreatePlatform(platform);

        var platformReadDto = _mapper.Map<PlatformReadDto>(platform);
        

        if (!await _platformRepo.SaveChangesAsync())
            return BadRequest();

        var platformPublishDto = _mapper.Map<PlatformPublishDto>(platformReadDto);
        platformPublishDto.Event = "Platform_Published";
        platformPublishDto.Id = platform.Id;
        _messageBusClient.PublishPlatform(platformPublishDto);
        platformReadDto.Id = platform.Id;
        return CreatedAtRoute(nameof(GetPlatformById), new {Id=platformReadDto.Id},platformReadDto);
    }
}