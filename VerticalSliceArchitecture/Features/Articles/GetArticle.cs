using FastEndpoints;
using MediatR;

namespace VerticalSliceArchitecture.Features.Articles;

public sealed class GetArticle : Endpoint<GetArticleQuery, IResult>
{
    private readonly ISender _sender;

    public GetArticle(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("api/article/{articleId}");
        AllowAnonymous();
    }

    public override async Task<IResult> ExecuteAsync(GetArticleQuery req, CancellationToken ct)
    {
        var result = await _sender.Send(req, ct);

        return result.Match<IResult>(
            TypedResults.Ok,
            m => TypedResults.NotFound(m.ToNotFoundResponse()));
    }
}