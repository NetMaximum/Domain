namespace NetMaximum.Domain;

public class Optional<T>
{
    public T? Value { get; }

    public bool HasValue { get; }

    private Optional()
    {
        this.HasValue = false;
    }

    private Optional(T value)
    {
        this.Value = value;
        this.HasValue = value != null;
    }

    public static Optional<T> None => new();

    public static Optional<T> Some(T value) => new(value);
}