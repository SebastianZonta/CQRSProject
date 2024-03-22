namespace VerticalSliceArchitecture.Shared
{
    //Reference: https://www.youtube.com/watch?v=KgfzM0QWHrQ
    public sealed class Result<TValue, TError>
        where TError : Error
    {
        public readonly TValue? Value;

        public readonly TError? Error;

        private readonly bool _isSuccess;

        private Result(TValue value)
        {
            Value = value;
            _isSuccess = true;
            Error = default;
        }

        private Result(TError error)
        {
            Value = default;
            Error = error;
            _isSuccess = false;
        }

        public static implicit operator Result<TValue, TError>(TValue value) => new(value);

        public static implicit operator Result<TValue, TError>(TError error) => new(error);

        public TResult Match<TResult>(Func<TValue, TResult> success, Func<TError, TResult> failure)
            => _isSuccess ? success(Value!) : failure(Error!);
    }
}
