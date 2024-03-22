using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public static class UpdateHeroErrors
    {
        public static readonly Error InvalidHero = Error.Validation("Hero.Update", "The values provided are invalid. The hero cannot be updated");

        public static readonly Error NonExistentHero = Error.NotFound("Hero.Update", "The hero for that id does not exist");
    }
}
