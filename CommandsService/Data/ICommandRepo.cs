using CommandsService.Models;

namespace CommandsService.Data;

public interface ICommandRepo
{
    bool SaveChanges();
    Task<bool> SaveChangesAsync();

    Task<IEnumerable<Platform>> GetAllPlatforms();
    Task CreatePlatform(Platform platform);
    Task<bool> PlatformExists(int platformId);
    Task<bool> ExternalPlatformExists(int externalPlatformId);

    Task<IEnumerable<Command>> GetCommandsForPlatform(int platformId);
    Task<Command> GetCommand(int platformId, int commandId);
    Task CreateCommand(int platformId, Command command);
}