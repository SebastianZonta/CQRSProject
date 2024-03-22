using FastEndpoints;
using MediatR;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed class UpdateHero : Endpoint<UpdateHeroCommand, IResult>
    {
        private readonly ISender _sender;

        public UpdateHero(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Put("api/hero/{id:int}");
            AllowAnonymous();
        }

        public override async Task<IResult> ExecuteAsync(UpdateHeroCommand req, CancellationToken ct)
        {
            var result = await _sender.Send(req);

            return result.Match<IResult>(
                TypedResults.Ok,
                error => TypedResults.BadRequest(error.ToGenericBadRequestResponse()));
        }
    }
}
