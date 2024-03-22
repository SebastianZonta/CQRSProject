using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceArchitecture.Entities;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Articles;

public sealed record GetArticleQuery([FromQuery(Name = "ArticleId")] Guid ArticleId) : IRequest<Result<Article?, Error>>;
