using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;

namespace CommandsService.Data;

public static class PrepDb
{
    public static async Task PrepPopulation(WebApplication application)
    {
        using (var scope = application.Services.CreateScope())
        {
            var grpcClient = scope.ServiceProvider.GetService(typeof(IPlatformDataClient)) as PlatformDataClient;

            var platforms = await grpcClient.ReturnAllPlatforms();
            
            await SeedData(scope.ServiceProvider.GetService<ICommandRepo>(),platforms);
            
        }
    }
    
    private static async Task SeedData(ICommandRepo commandRepo, IEnumerable<Platform> platforms)
    {
        foreach (var platform in platforms)
        {
            if (!await commandRepo.ExternalPlatformExists(platform.ExternalId))
                await commandRepo.CreatePlatform(platform);
        }

        await commandRepo.SaveChangesAsync();
    }
}