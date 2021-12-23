using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

public class PlatformEfRepo : IPlatformRepo
{
    private readonly AppDbContext _appDbContext;
    

    public PlatformEfRepo(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _appDbContext.SaveChangesAsync() >= 0;
    }

    public bool SaveChanges()
    {
        return _appDbContext.SaveChanges() >= 0;
    }

    public async Task<IEnumerable<Platform>> getAllPlatforms()
    {
        return await _appDbContext.Platforms.ToListAsync();
    }

    public async Task<Platform> GetPlatformById(int id)
    {
        return await _appDbContext.Platforms.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreatePlatform(Platform platform)
    {
        await _appDbContext.Platforms.AddAsync(platform);
    }
}