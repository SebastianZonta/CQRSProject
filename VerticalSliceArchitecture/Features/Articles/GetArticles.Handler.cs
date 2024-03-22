using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Database;
using VerticalSliceArchitecture.Entities;

namespace VerticalSliceArchitecture.Features.Articles
{
    public sealed class GetArticlesHandler : IRequestHandler<GetArticlesQuery, ICollection<Article>>
    {
        private readonly ApplicationDbContext _context;

        public GetArticlesHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Article>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            return
                await _context
                .Articles
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
