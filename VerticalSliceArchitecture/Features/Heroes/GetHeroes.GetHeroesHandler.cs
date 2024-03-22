using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Database;
using VerticalSliceArchitecture.Entities;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed class GetHeroesHandler : IRequestHandler<GetHeroesQuery, Result<List<Hero>, Error>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetHeroesHandler> _logger;

        public GetHeroesHandler(ApplicationDbContext context,
            ILogger<GetHeroesHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result<List<Hero>, Error>> Handle(GetHeroesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Heroes.ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error ocurred with message: {Message}", e.Message);
                return Error.Failure("InternalServerError", e.Message);
            }
        }
    }
}
