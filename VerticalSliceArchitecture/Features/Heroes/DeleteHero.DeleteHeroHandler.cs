using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Database;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed class DeleteHeroHandler : IRequestHandler<DeleteHeroCommand, Result<int, Error>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<DeleteHeroCommand> _validator;
        private readonly ILogger<DeleteHeroHandler> _logger;

        public DeleteHeroHandler(ApplicationDbContext context,
            IValidator<DeleteHeroCommand> validator,
            ILogger<DeleteHeroHandler> logger)
        {
            _context = context;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<int, Error>> Handle(DeleteHeroCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    return DeleteHeroErrors.InvalidHero;

                var rowCount = await _context.Heroes.Where(e => e.Id == request.HeroId).ExecuteDeleteAsync(cancellationToken);
                if (rowCount is 0)
                    return DeleteHeroErrors.InvalidHero;

                return request.HeroId;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred when trying to delete the hero for Id: {HeroId} with exception message: {Message}", request.HeroId.ToString(), e.Message);;

                return DeleteHeroErrors.ErrorOnDelete;
            }
        }
    }
}
