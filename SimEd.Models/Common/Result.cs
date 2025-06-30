namespace SimEd.Models.Common;

public record struct Result<T>(T Value, string Error)
{
    public static implicit operator Result<T>(T value)
        => new(value, string.Empty);

    public static Result<T> Ok(T value)
        => new(value, string.Empty);

    public static Result<T> Err(string err)
        => new(default, err);

    public bool IsOk => string.IsNullOrEmpty(Error);
}