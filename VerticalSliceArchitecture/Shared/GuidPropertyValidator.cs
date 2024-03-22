using FluentValidation;
using FluentValidation.Validators;

namespace VerticalSliceArchitecture.Shared
{
    public sealed class GuidPropertyValidator<T> : PropertyValidator<T, Guid>
    {
        public override string Name => "GuidValidator";

        public override bool IsValid(ValidationContext<T> context, Guid value)
        {
            if (value != Guid.Empty)
                return true;

            context.AddFailure("The {PropertyName} must be a valid guid");

            return false;
        }
    }

    public static class GuidPropertyValidatorExtensions
    {
        public static void IsGuidValid<T>(this IRuleBuilder<T, Guid> ruleBuilder)
        {
            ruleBuilder.SetValidator(new GuidPropertyValidator<T>());
        }
    }
}
