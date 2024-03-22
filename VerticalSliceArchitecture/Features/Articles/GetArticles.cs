using FastEndpoints;
using MediatR;

namespace VerticalSliceArchitecture.Features.Articles
{
    public sealed class GetArticles : Endpoint<GetArticlesQuery, IResult>
    {
        private readonly ISender _sender;

        public GetArticles(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Get("api/article");
            AllowAnonymous();
        }

        public override async Task<IResult> ExecuteAsync(GetArticlesQuery req, CancellationToken ct)
        {
            var result = await _sender.Send(req, ct);

            return TypedResults.Ok(result);
        }
    }
}
