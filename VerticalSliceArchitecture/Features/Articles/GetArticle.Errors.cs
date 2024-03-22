using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Articles;

public static class GetArticleErrors
{
    public static readonly Error ArticleNotFound = Error.NotFound("Article.NotFound", "The article does not exists");
}

