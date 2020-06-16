using Storage;

namespace Services
{
    public abstract class TransactionServices<T> : Services<T>
    {
        protected TransactionServices(
        IStorage storage
        ) : base(storage) { }
    }
}