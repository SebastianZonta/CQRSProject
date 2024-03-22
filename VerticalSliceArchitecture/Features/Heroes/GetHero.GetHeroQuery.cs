using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceArchitecture.Entities;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed record GetHeroQuery([FromRoute] int HeroId) : IRequest<Result<Hero, Error>>;
}
