using MediatR;
using PlatformService.Dtos;

namespace PlatformService.Queries;

public class GetPlatformByIdQuery: IRequest<PlatformReadDto>
{
    public int Id { get; }
    
    public GetPlatformByIdQuery(int id)
    {
        Id = id;
    }
}