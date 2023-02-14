using NetMaximum.Domain.Validation;

namespace NetMaximum.Domain
{
    public class DomainException : Exception
    {
        public IEnumerable<ValidationError> Errors { get; } = Enumerable.Empty<ValidationError>();

        public DomainException() : base()
        {
        }
        
        public DomainException(IEnumerable<ValidationError> results)
        {
            Errors = results;
        }
    }
}