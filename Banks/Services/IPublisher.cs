namespace Banks.Services
{
    public interface IPublisher
    {
        public void Subscribe(ISubscriber subscriber);

        public void Unsubscribe(ISubscriber subscriber);

        public void Inform(string message);
    }
}