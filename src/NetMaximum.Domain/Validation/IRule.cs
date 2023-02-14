namespace NetMaximum.Domain.Validation
{
    public interface IRule<in TEntity>
    {
        string ErrorMessage { get; }

        bool Validate(TEntity entity);
    }
}
