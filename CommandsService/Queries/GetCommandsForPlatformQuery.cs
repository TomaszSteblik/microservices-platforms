using CommandsService.Dtos;
using MediatR;

namespace CommandsService.Queries;

public class GetCommandsForPlatformQuery : IRequest<IEnumerable<CommandReadDto>>
{
    public int Id { get; }

    public GetCommandsForPlatformQuery(int id)
    {
        Id = id;
    }
}