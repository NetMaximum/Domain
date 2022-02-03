namespace NetMaximum.Domain.EventSourced
{
    public abstract class EventSourcedAggregateRoot : AggregateRoot, IInternalEventHandler 
    {
        private readonly List<object> _events = new();
        
        /// <summary>
        /// Current version of the aggregate in memory, might not be the stored version.
        /// </summary>
        public int Version { get; private set; } = 0;

        /// <summary>
        /// Stores the version the aggregate was loaded with.
        /// </summary>
        public int LoadedVersion { get; private set; } = 0;

        
        protected EventSourcedAggregateRoot(Guid aggregateId) : base(aggregateId)
        {
        }
        
        void IInternalEventHandler.Handle(object @event) => When(@event);

        protected abstract void When(object @event);

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _events.Add(@event);
            Version++;
        }

        /// <summary>
        /// Gets an all events that have been applied to the aggregate.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetEvents() => _events.AsReadOnly();

        /// <summary>
        /// Loads the events back into the aggregate, ignoring validation until the final load.
        /// </summary>
        /// <param name="history"></param>
        public void Load(IEnumerable<object> history)
        {
            foreach (var e in history)
            {
                When(e);
                _events.Add(e);
                Version++;
            }
            
            EnsureValidState();
            
            // Stores the loaded version, for concurrency reasons.
            LoadedVersion = Version;
        }

        /// <summary>
        /// Removes all events from the 
        /// </summary>
        public void ClearEvents()
        {
            _events.Clear();
            Version = 0;
            LoadedVersion = 0;
        }
      
        /// <summary>
        /// Ensures the aggregate is in a valid state after transition.
        /// </summary>
        protected abstract void EnsureValidState();

        protected void ApplyToEntity(IInternalEventHandler entity, object @event) => entity?.Handle(@event);
    }
}