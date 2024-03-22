using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Database;
using VerticalSliceArchitecture.Entities;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Articles
{
    public sealed class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, Result<Article?, Error>>
    {
        private readonly ApplicationDbContext _context;

        public GetArticleQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Article?, Error>> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.SingleOrDefaultAsync(e => e.Id == request.ArticleId, cancellationToken);

            return article is null ? GetArticleErrors.ArticleNotFound : article;
        }
    }
}
