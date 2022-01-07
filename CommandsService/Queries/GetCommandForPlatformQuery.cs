using CommandsService.Dtos;
using MediatR;

namespace CommandsService.Queries;

public class GetCommandForPlatformQuery : IRequest<CommandReadDto>
{
    public int CommandId { get; }
    public int PlatformId { get; }

    public GetCommandForPlatformQuery(int platformId, int commandId)
    {
        PlatformId = platformId;
        CommandId = commandId;
    }
}