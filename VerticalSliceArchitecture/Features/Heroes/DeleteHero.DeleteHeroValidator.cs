using FluentValidation;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed class DeleteHeroValidator : AbstractValidator<DeleteHeroCommand>
    {
        public DeleteHeroValidator()
        {
            RuleFor(e => e.HeroId).GreaterThan(0);
        }
    }
}
