using System.Reflection;

namespace NetMaximum.Domain
{
    public abstract class EventSourcedAggregateRoot<T> : AggregateRoot, IInternalEventHandler 
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

        /// <summary>
        /// Here to allow creation via things such as serialisation and EF.
        /// </summary>
        protected EventSourcedAggregateRoot() : base()
        {
        }

        protected EventSourcedAggregateRoot(Guid aggregateId) : base(aggregateId)
        {
        }
        
        protected EventSourcedAggregateRoot(Guid aggregateId, IEnumerable<object> @events) : base(aggregateId)
        {
            Load(aggregateId, @events);
        }
        
        /// <summary>
        /// Create a new instance of the aggregate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // public static T Create(Guid id)
        // {
        //     // Finds the ctor that accepts a Guid id.
        //     var c= typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance, 
        //         null, 
        //         new[] { typeof(Guid) }, 
        //         null
        //     );
        //
        //     // Todo : Add an error
        //     
        //     return (T)c.Invoke(new object[] {id});
        // }
        //
        // /// <summary>
        // /// Create a new instance of the aggregate, loading events (note this bypasses validation)
        // /// </summary>
        // /// <param name="id"></param>
        // /// <returns></returns>
        // public static T Create(Guid id, IEnumerable<object> @events)
        // {
        //     // Finds the second ctor which accepts a list of events.
        //     var c= typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance, 
        //         null, 
        //         new[] { typeof(Guid), typeof(IEnumerable<object>) }, 
        //         null
        //     );
        //
        //     // Todo : Add an error
        //     
        //     return (T)c.Invoke(new object[] {id, @events});
        // }
        
        void IInternalEventHandler.Handle(object @event) => When(@event);

        protected abstract void When(object @event);

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _events.Add(@event);
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
        public void Load(Guid aggregateId, IEnumerable<object> history)
        {
            foreach (var e in history)
            {
                When(e);
                _events.Add(e);
                Version++;
            }
            
            // EnsureValidState();
            
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
        }
      
        /// <summary>
        /// Ensures the aggregate is in a valid state after transition.
        /// </summary>
        protected abstract void EnsureValidState();

        protected void ApplyToEntity(IInternalEventHandler entity, object @event) => entity?.Handle(@event);
    }
}