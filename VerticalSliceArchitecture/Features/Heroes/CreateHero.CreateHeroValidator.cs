using FluentValidation;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public sealed class CreateHeroValidator : AbstractValidator<CreateHeroCommand>
    {
        public CreateHeroValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Power).NotEmpty();
        }
    }
}
