using FastEndpoints;
using MediatR;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Articles;

public sealed class CreateArticle : Endpoint<CreateArticleCommand, IResult>
{
    private readonly ISender _sender;

    public CreateArticle(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put("api/article");
        AllowAnonymous();
    }

    public override async Task<IResult> ExecuteAsync(CreateArticleCommand req, CancellationToken ct)
    {
        var result = await _sender.Send(req, ct);
        return result.Match<IResult>(
            guid => TypedResults.Ok(guid),
            error => TypedResults.BadRequest(error.ToGenericBadRequestResponse()));
    }
}
