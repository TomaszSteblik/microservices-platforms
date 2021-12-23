using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _mapper = mapper;
        _scopeFactory = scopeFactory;
    }
    
    public async Task ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);

        if (eventType == EventType.PlatformPublished)
            await AddPlatform(message);

    }

    private async Task AddPlatform(string message)
    {
        using var scope = _scopeFactory.CreateScope();
        
        var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
        
        var platformPublishDto = JsonSerializer.Deserialize<PlatformPublishDto>(message);

        var platform = _mapper.Map<Platform>(platformPublishDto);
        if (!await repo.ExternalPlatformExists(platform.ExternalId))
        {
            await repo.CreatePlatform(platform);
            await repo.SaveChangesAsync();
        }

    }

    private EventType DetermineEvent(string notificationMessage)
    {
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

        return eventType?.Event == "Platform_Published" ? EventType.PlatformPublished : EventType.Undetermined;
    }
}
enum EventType
{
    PlatformPublished,
    Undetermined
}