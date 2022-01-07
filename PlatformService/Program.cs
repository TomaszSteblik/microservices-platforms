using MediatR;
using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.SyncDataServices;
using PlatformService.SyncDataServices.Grpc;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => 
        opt.UseNpgsql(builder.Configuration.GetConnectionString("PlatformsConn")));
builder.Services.AddScoped<IPlatformRepo,PlatformEfRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<ICommandDataClient,HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddGrpc();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(z=>z.AllowAnyOrigin());
}
else
{
    app.UseHttpsRedirection();
}
using var scope = app.Services.CreateScope();
(scope.ServiceProvider.GetService(typeof(AppDbContext)) as AppDbContext)!.Database.Migrate();
PrepDb.PrepPopulation(app);

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<GrpcPlatformService>();


app.MapGet("/protos/platforms.proto", async context =>
{
    await context.Response.WriteAsync(await System.IO.File.ReadAllTextAsync("Protos/platforms.proto"));
});

app.Run();

