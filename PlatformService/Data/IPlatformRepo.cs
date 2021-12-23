using PlatformService.Models;

namespace PlatformService.Data;

public interface IPlatformRepo
{
    Task<bool> SaveChangesAsync();
    bool SaveChanges();
    Task<IEnumerable<Platform>> getAllPlatforms();
    Task<Platform> GetPlatformById(int id);
    Task CreatePlatform(Platform platform);
}