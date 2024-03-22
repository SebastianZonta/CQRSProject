using FastEndpoints;
using MediatR;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes;

public sealed class GetHeroes : EndpointWithoutRequest<IResult>
{
    private readonly ISender _sender;

    public GetHeroes(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("api/hero");
        AllowAnonymous();
    }

    public override async Task<IResult> ExecuteAsync(CancellationToken ct)
    {
        var result = await _sender.Send(new GetHeroesQuery(), ct);
        return result.Match<IResult>(
            TypedResults.Ok,
            m => TypedResults.Problem(m.ToGenericInternalServerErrorResponse()));
    }
}

