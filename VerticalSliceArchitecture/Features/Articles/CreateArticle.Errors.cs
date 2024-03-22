using Microsoft.AspNetCore.Mvc;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Articles;

public static class ArticleErrors
{
    public static readonly Error InvalidArticle = Error.Validation("Article.Validation", "The values provided are invalid");

    public static ProblemDetails ToNotFoundResponse(this Error error)
    {
        return new ProblemDetails
        {
            Title = "Article not found",
            Type = error.Code,
            Detail = error.Description,
            Status = StatusCodes.Status404NotFound,
            Extensions = { { nameof(error), error } }
        };
    }
}
