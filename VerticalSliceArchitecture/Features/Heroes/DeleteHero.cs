using FastEndpoints;
using MediatR;
using VerticalSliceArchitecture.Features.Articles;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed class DeleteHero : Endpoint<DeleteHeroCommand, IResult>
    {
        private readonly ISender _sender;

        public DeleteHero(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Delete("api/hero/{heroId:int}");
            AllowAnonymous();
        }

        public override async Task<IResult> ExecuteAsync(DeleteHeroCommand req, CancellationToken ct)
        {
            var result = await _sender.Send(req, ct);
            return result.Match<IResult>(
                TypedResults.Ok,
                m =>
                {
                    return m.Code switch
                    {
                        DeleteHeroErrors.ErrorCodeNames.ErrorOnDelete => TypedResults.Problem(m.ToGenericInternalServerErrorResponse()),
                        DeleteHeroErrors.ErrorCodeNames.NonExistentHero => TypedResults.NotFound(m.ToNotFoundResponse()),
                        DeleteHeroErrors.ErrorCodeNames.InvalidHero => TypedResults.BadRequest(m.ToGenericBadRequestResponse()),
                        _ => TypedResults.Problem(m.ToGenericInternalServerErrorResponse())
                    };
                });
        }
    }
}
