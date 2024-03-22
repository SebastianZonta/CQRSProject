using MediatR;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed class CreateHeroCommand : IRequest<Result<int, Error>>
    {
        public string Name { get; set; } = string.Empty;

        public string Power { get; set; } = string.Empty;

        public bool IsAlive { get; set; } = false;
    }
}
