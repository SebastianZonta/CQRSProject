using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceArchitecture.Entities;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed class UpdateHeroCommand : IRequest<Result<Hero, Error>>
    {
        [FromRoute]
        public int Id { get; set; }

        [FromBody]
        public string Name { get; set; } = string.Empty;

        [FromBody]
        public string Power { get; set; } = string.Empty;

        [FromBody]
        public bool IsAlive { get; set; } = false;
    }
}
