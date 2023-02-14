namespace NetMaximum.Domain
{
    public abstract class AggregateId{
    
    }
    
    public abstract class AggregateId<T> : AggregateId where T : AggregateRoot
    {
        protected AggregateId(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(
                    nameof(value), 
                    "The Id cannot be empty");
            
            Value = value;
        }

        public string Value { get; }
        
        public static implicit operator string(AggregateId<T> self) => self.Value;

        public override string ToString() => Value.ToString();
    }
}