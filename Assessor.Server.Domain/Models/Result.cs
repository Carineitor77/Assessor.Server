namespace Assessor.Server.Domain.Models;

public class Result<TValue, TError> where TError : Error
{
    public bool IsSuccess { get; }
    public TValue Value { get; } = default!;
    public TError Error { get; } = default!;

    private Result(TValue value)
    {
        IsSuccess = true;
        Value = value;
    }

    private Result(TError error)
    {
        IsSuccess = false;
        Error = error;
    }
    
    public static implicit operator Result<TValue, TError>(TValue value) => new(value);

    public static implicit operator Result<TValue, TError>(TError error) => new(error);
}