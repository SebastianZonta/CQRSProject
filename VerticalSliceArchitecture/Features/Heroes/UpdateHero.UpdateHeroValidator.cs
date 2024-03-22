using FluentValidation;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public class UpdateHeroValidator : AbstractValidator<UpdateHeroCommand>
    {
        public UpdateHeroValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Power).NotEmpty();
        }
    }
}
