using Storage;

namespace Services
{
    public abstract class BalanceServices<T> : Services<T>
    {
        protected BalanceServices(
            IStorage storage
        ) : base(storage) { }
    }
}