namespace NetMaximum.Domain
{
    public abstract class Entity<TId> : IInternalEventHandler where TId : Value<TId>
    {
        public TId Id { get; protected set; }
        
        void IInternalEventHandler.Handle(object @event) => When(@event);

        protected abstract void When(object @event);
        
    }
}