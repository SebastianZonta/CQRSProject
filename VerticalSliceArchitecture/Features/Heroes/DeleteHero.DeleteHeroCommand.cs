using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed record DeleteHeroCommand([FromRoute] int HeroId) : IRequest<Result<int, Error>>;
}
