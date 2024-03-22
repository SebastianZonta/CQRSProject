using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public static class DeleteHeroErrors
    {
        public static class ErrorCodeNames
        {
            public const string InvalidHero = "Hero.Validation";
            public const string ErrorOnDelete = "Internal.Server.Error";
            public const string NonExistentHero = "Hero.Delete";

        }

        public static readonly Error InvalidHero = Error.Validation(ErrorCodeNames.InvalidHero, "The values provided are invalid");

        public static readonly Error ErrorOnDelete = Error.Failure(ErrorCodeNames.ErrorOnDelete, "Error trying to delete the hero");

        public static readonly Error NonExistentHero = Error.NotFound(ErrorCodeNames.NonExistentHero, "There is not hero to delete with that ID");
    }
}
