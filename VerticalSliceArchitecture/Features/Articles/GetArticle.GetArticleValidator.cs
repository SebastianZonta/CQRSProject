using FluentValidation;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Articles
{
    public sealed class GetArticleValidator : AbstractValidator<GetArticleQuery>
    {
        public GetArticleValidator()
        {
            RuleFor(e => e.ArticleId).IsGuidValid();
        }
    }
}
