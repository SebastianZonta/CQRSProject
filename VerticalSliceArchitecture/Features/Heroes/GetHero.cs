using FastEndpoints;
using MediatR;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes;

public sealed class GetHero : Endpoint<GetHeroQuery, IResult>
{
    private readonly ISender _sender;

    public GetHero(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("api/hero/{heroId:int}");
        AllowAnonymous();
    }

    public override async Task<IResult> ExecuteAsync(GetHeroQuery req, CancellationToken ct)
    {
        var result = await _sender.Send(req, ct);
        return result.Match<IResult>(
            TypedResults.Ok,
            m => TypedResults.Problem(m.ToGenericInternalServerErrorResponse()));
    }
}