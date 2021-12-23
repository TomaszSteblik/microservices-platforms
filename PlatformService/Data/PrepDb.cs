using System.Diagnostics;
using PlatformService.Models;

namespace PlatformService.Data;

public static class PrepDb
{
    public static void PrepPopulation(WebApplication builder)
    {
        using (var scope = builder.Services.CreateScope())
        {
            SeedData(scope.ServiceProvider.GetService(typeof(AppDbContext)) as AppDbContext);
        }
        
    }

    private static void SeedData(AppDbContext appDbContext)
    {
        if (appDbContext.Platforms.Any())
            return;
        
        appDbContext.AddRange(
            new Platform() {Name = "Dot Net", Publisher = "Microsoft", Cost = "Free"},
                        new Platform() {Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free"},
                        new Platform() {Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free"}
            );
        
        appDbContext.SaveChanges();
    }
}