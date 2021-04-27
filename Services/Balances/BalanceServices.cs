using Storage;

namespace Services.Balances
{
    public abstract class BalanceServices<T> : Services<T>
    {
        protected BalanceServices(
            IStorage[] storage
        ) : base(storage) { }

        protected virtual decimal Compute() => 0;
    }
}