using MediatR;
using PlatformService.Dtos;

namespace PlatformService.Commands;

public class CreatePlatformCommand : IRequest<PlatformReadDto>
{
    public CreatePlatformCommand(PlatformCreateDto platformCreateDto)
    {
        PlatformCreateDto = platformCreateDto;
    }

    public PlatformCreateDto PlatformCreateDto { get; }
    
    
}