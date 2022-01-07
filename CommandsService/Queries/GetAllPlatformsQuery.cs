using CommandsService.Dtos;
using MediatR;

namespace CommandsService.Queries;

public class GetAllPlatformsQuery : IRequest<IEnumerable<PlatformReadDto>>
{
    
}