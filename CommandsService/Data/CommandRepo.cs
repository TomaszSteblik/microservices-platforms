using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data;

public class CommandRepo : ICommandRepo
{

    private readonly AppDbContext _dbContext;


    public CommandRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool SaveChanges()
    {
        return _dbContext.SaveChanges() > 0;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Platform>> GetAllPlatforms()
    {
        return await _dbContext.Platforms.ToListAsync();
    }

    public async Task CreatePlatform(Platform platform)
    {
        if (platform is null)
            return;
        await _dbContext.Platforms.AddAsync(platform);
    }

    public async Task<bool> PlatformExists(int platformId)
    {
        return await _dbContext.Platforms.AnyAsync(p => p.Id == platformId);
    }

    public async Task<bool> ExternalPlatformExists(int externalPlatformId)
    {
        return await _dbContext.Platforms.AnyAsync(p => p.ExternalId == externalPlatformId);
    }

    public async Task<IEnumerable<Command>> GetCommandsForPlatform(int platformId)
    {
        return await _dbContext.Commands
            .Where(c => c.PlatformId == platformId)
            .ToListAsync();
    }

    public async Task<Command> GetCommand(int platformId, int commandId)
    {
        return await _dbContext.Commands.FirstOrDefaultAsync(c => c.PlatformId == platformId && c.Id == commandId);
    }

    public async Task CreateCommand(int platformId, Command command)
    {
        if (command is null)
            return;

        command.PlatformId = platformId;
        
        await _dbContext.Commands.AddAsync(command);
    }
}