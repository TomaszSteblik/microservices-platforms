using AutoMapper;
using MediatR;
using PlatformService.AsyncDataServices;
using PlatformService.Commands;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Handlers;

public class CreatePlatformHandler : IRequestHandler<CreatePlatformCommand, PlatformReadDto>
{
    private readonly IPlatformRepo _platformRepo;
    private readonly IMapper _mapper;
    private readonly IMessageBusClient _messageBusClient;


    public CreatePlatformHandler(IMapper mapper, IPlatformRepo platformRepo, IMessageBusClient messageBusClient)
    {
        _mapper = mapper;
        _platformRepo = platformRepo;
        _messageBusClient = messageBusClient;
    }

    public async Task<PlatformReadDto> Handle(CreatePlatformCommand request, CancellationToken cancellationToken)
    {
        var platform = _mapper.Map<Platform>(request.PlatformCreateDto);

        await _platformRepo.CreatePlatform(platform);
        await _platformRepo.SaveChangesAsync();

        var platformReadDto = _mapper.Map<PlatformReadDto>(platform);
        
        var platformPublishDto = _mapper.Map<PlatformPublishDto>(platformReadDto);
        platformPublishDto.Event = "Platform_Published";
        platformPublishDto.Id = platform.Id;
        _messageBusClient.PublishPlatform(platformPublishDto);
        
        platformReadDto.Id = platform.Id;

        return platformReadDto;
    }
}