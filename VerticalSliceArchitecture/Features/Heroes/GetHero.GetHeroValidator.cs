using FluentValidation;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed class GetHeroValidator : AbstractValidator<GetHeroQuery>
    {
        public GetHeroValidator()
        {
            RuleFor(e => e.HeroId).GreaterThan(0);
        }
    }
}
