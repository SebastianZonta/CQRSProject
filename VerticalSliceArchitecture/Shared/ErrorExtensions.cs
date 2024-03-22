using Microsoft.AspNetCore.Mvc;

namespace VerticalSliceArchitecture.Shared
{
    public static class ErrorExtensions
    {
        public static ProblemDetails ToGenericBadRequestResponse(this Error error)
        {
            return new ProblemDetails
            {
                Title = "Invalid information",
                Type = error.Code,
                Detail = error.Description,
                Status = StatusCodes.Status400BadRequest,
                Extensions = { { nameof(error), error } }
            };
        }

        public static ProblemDetails ToGenericInternalServerErrorResponse(this Error error)
        {
            return new ProblemDetails
            {
                Title = "Server error",
                Type = error.Code,
                Detail = error.Description,
                Status = StatusCodes.Status500InternalServerError,
                Extensions = { { nameof(error), error } }
            };
        }
    }
}
