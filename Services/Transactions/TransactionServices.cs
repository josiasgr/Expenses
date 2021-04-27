using Storage;

namespace Services.Transactions
{
    public abstract class TransactionServices<T> : Services<T>
    {
        protected TransactionServices(
        IStorage[] storage
        ) : base(storage) { }
    }
}