using FluentValidation;
using MediatR;
using VerticalSliceArchitecture.Database;
using VerticalSliceArchitecture.Entities;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed class CreateHeroHandler : IRequestHandler<CreateHeroCommand, Result<int, Error>>
    {
        private readonly IValidator<CreateHeroCommand> _validator;
        private readonly ApplicationDbContext _context;

        public CreateHeroHandler(IValidator<CreateHeroCommand> validator, ApplicationDbContext context)
        {
            _validator = validator;
            _context = context;
        }
        public async Task<Result<int, Error>> Handle(CreateHeroCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return UpdateHeroErrors.InvalidHero;
            }

            var hero = new Hero
            {
                Name = request.Name,
                Power = request.Power,
                IsAlive = request.IsAlive
            };

            await _context.Heroes.AddAsync(hero, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return hero.Id;
        }
    }
}
