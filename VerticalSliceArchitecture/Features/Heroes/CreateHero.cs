using FastEndpoints;
using MediatR;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed class CreateHero : Endpoint<CreateHeroCommand, IResult>
    {
        private readonly ISender _sender;

        public CreateHero(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Post("/api/hero");
            AllowAnonymous();
        }

        public override async Task<IResult> ExecuteAsync(CreateHeroCommand req, CancellationToken ct)
        {
            var result = await _sender.Send(req, ct);
            return result.Match<IResult>(
                TypedResults.Ok,
                error => TypedResults.BadRequest(error.ToGenericBadRequestResponse()));
        }
    }
}
