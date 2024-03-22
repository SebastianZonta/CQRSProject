using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Database;
using VerticalSliceArchitecture.Entities;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed class GetHeroHandler : IRequestHandler<GetHeroQuery, Result<Hero, Error>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetHeroHandler> _logger;
        private readonly IValidator<GetHeroQuery> _validator;

        public GetHeroHandler(ApplicationDbContext context,
            ILogger<GetHeroHandler> logger,
            IValidator<GetHeroQuery> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Hero, Error>> Handle(GetHeroQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    return Error.Validation("Validation.Failure", "The id provided is incorrect. Must be greater than 0");

                var hero = await _context.Heroes.SingleOrDefaultAsync(h => h.Id == request.HeroId, cancellationToken);

                return
                    hero is null
                        ? Error.NotFound("Hero.NotFound", "There is no hero for that id")
                        : hero;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error ocurred with message: {Message}", e.Message);
                return Error.Failure("InternalServerError", e.Message);
            }
        }
    }
}
