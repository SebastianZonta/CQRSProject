using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Database;
using VerticalSliceArchitecture.Entities;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed class UpdateHeroHandler : IRequestHandler<UpdateHeroCommand, Result<Hero, Error>>
    {
        private readonly IValidator<UpdateHeroCommand> _validator;
        private readonly ApplicationDbContext _context;

        public UpdateHeroHandler(IValidator<UpdateHeroCommand> validator, ApplicationDbContext context)
        {
            _validator = validator;
            _context = context;
        }

        public async Task<Result<Hero, Error>> Handle(UpdateHeroCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return UpdateHeroErrors.InvalidHero;

            //var hero = await _context.Heroes.SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            var hero = await _context.Heroes.SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (hero is null)
                return UpdateHeroErrors.NonExistentHero;

            hero.Power = request.Power;
            hero.IsAlive = request.IsAlive;
            hero.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return hero;
        }
    }
}
