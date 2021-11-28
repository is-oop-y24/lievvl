namespace Banks.Services
{
    public interface ISubscriber
    {
        void Update(string message);
    }
}