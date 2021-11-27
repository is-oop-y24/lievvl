namespace Banks.Services
{
    public interface IPublisher
    {
        void Subscribe(ISubscriber subscriber);

        void Unsubscribe(ISubscriber subscriber);

        void Inform(string message);
    }
}