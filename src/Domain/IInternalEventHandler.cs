namespace Domain
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}