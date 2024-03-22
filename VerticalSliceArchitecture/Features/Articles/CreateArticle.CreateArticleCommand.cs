using MediatR;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Articles;

public sealed class CreateArticleCommand : IRequest<Result<Guid, Error>>
{
    public string Title { get; set; } = String.Empty;

    public string Content { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = new();
}

