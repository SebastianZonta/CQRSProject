using FluentValidation;
using MediatR;
using VerticalSliceArchitecture.Database;
using VerticalSliceArchitecture.Entities;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Articles;

public sealed class CreateArticleHandler : IRequestHandler<CreateArticleCommand, Result<Guid, Error>>
{
    private readonly ApplicationDbContext _context;
    private readonly IValidator<CreateArticleCommand> _validator;

    public CreateArticleHandler(ApplicationDbContext context,
        IValidator<CreateArticleCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<Guid, Error>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid is false)
        {
            return ArticleErrors.InvalidArticle;
        }

        var article = new Article
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Content = request.Content,
            Tags = request.Tags,
            CreatedOnUtc = DateTime.UtcNow
        };

        await _context.AddAsync(article, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return article.Id;
    }
}