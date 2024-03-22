using MediatR;
using VerticalSliceArchitecture.Entities;

namespace VerticalSliceArchitecture.Features.Articles
{
    public sealed record GetArticlesQuery : IRequest<ICollection<Article>>;
}
