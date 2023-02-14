namespace NetMaximum.Domain.Validation
{
    public class ValidationError
    {
        public string Name { get; set; }
        public IEnumerable<string> Message { get; } = new List<string>();

        public ValidationError(string name, string message)
        {
            Name = name;
            Message = new List<string> { message };
        }
    }
}
