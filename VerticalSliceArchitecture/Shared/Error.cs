using System.Text.Json.Serialization;

namespace VerticalSliceArchitecture.Shared;

public class Error
{
    public static Error InternalServerError = Failure("Error.InternalServerError", "An error occurred when processing the request");

    public string Code { get; }

    public string Description { get; }

    [JsonIgnore]
    public ErrorType ErrorType { get; }

    private Error(string code, string description, ErrorType error)
    {
        Code = code;
        Description = description;
        ErrorType = error;
    }

    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);

    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);
}

public enum ErrorType
{
    Failure,
    Validation,
    NotFound
}
