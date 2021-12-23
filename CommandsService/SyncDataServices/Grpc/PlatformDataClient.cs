using AutoMapper;
using CommandService;
using CommandsService.Models;
using Grpc.Net.Client;

namespace CommandsService.SyncDataServices.Grpc;

public class PlatformDataClient : IPlatformDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public PlatformDataClient(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<Platform>> ReturnAllPlatforms()
    {
        var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
        var client = new GrpcPlatform.GrpcPlatformClient(channel);

        var request = new GetAllRequest();

        var reply = await client.GetAllPlatformsAsync(request);

        return _mapper.Map<IEnumerable<Platform>>(reply.Platform);

    }
}