namespace NetMaximum.Domain
{
    public abstract class EventSourcedAggregateRoot : AggregateRoot, IInternalEventHandler
    {
        private readonly List<object> _changes = new();

        public int Version { get; private set; } = -1;
        public HashSet<string> Transitions { get; } = new();
        
        protected EventSourcedAggregateRoot(Guid aggregateId) : base(aggregateId)
        {
            
        }
        
        void IInternalEventHandler.Handle(object @event) => When(@event);

        protected abstract void When(object @event);

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _changes.Add(@event);
        }

        public IEnumerable<object> GetChanges() => _changes.AsEnumerable();

        public void Load(IEnumerable<object> history)
        {
            foreach (var e in history)
            {
                When(e);
                Version++;
            }
        }

        public void ClearChanges() => _changes.Clear();
      
        protected abstract void EnsureValidState();

        protected void ApplyToEntity(IInternalEventHandler entity, object @event) => entity?.Handle(@event);

        
    }
}