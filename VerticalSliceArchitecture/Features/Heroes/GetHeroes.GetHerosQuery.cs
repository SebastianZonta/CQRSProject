using MediatR;
using VerticalSliceArchitecture.Entities;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed record GetHeroesQuery() : IRequest<Result<List<Hero>, Error>>;
}
