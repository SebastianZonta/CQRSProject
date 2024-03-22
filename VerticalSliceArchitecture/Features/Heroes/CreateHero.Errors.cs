using Microsoft.AspNetCore.Mvc;
using VerticalSliceArchitecture.Shared;

namespace VerticalSliceArchitecture.Features.Heroes
{
    public static class CreateHeroErrors
    {
        public static readonly Error InvalidHero = Error.Validation("Hero.Validation", "The values provided are invalid");
    }
}
