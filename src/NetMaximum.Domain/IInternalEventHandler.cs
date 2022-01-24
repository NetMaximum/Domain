namespace NetMaximum.Domain
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}