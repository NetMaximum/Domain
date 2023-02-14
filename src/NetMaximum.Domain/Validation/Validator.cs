namespace NetMaximum.Domain.Validation
{
    public class Validator<TEntity> : IValidator<TEntity> where TEntity : class
    {
        public Dictionary<string, IRule<TEntity>> Rules { get; }

        public Validator() => Rules = new Dictionary<string, IRule<TEntity>>();

        public ValidationResult Validate(TEntity entity)
        {
            var validation = new ValidationResult();
            foreach(var rule in Rules)
                if (!rule.Value.Validate(entity))
                    validation.Add(new ValidationError(rule.Key, rule.Value.ErrorMessage));

            return validation;
        }

        protected virtual void Add(string name, IRule<TEntity> rule) => Rules.Add(name, rule);

        protected IRule<TEntity> GetRule(string name) => Rules[name];

        protected virtual void Remove(string name) => Rules.Remove(name);
    }
}
