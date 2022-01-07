using CommandsService.Dtos;
using MediatR;

namespace CommandsService.Commands;

public class CreateCommandCommand : IRequest<CommandReadDto>
{
    public int PlatformId { get; set; }
    public CommandCreateDto CommandCreateDto { get; set; }

    public CreateCommandCommand(CommandCreateDto commandCreateDto, int platformId)
    {
        CommandCreateDto = commandCreateDto;
        PlatformId = platformId;
    }
}