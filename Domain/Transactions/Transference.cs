using System;

namespace Domain.Transactions
{
    public class Transference : Transaction, IEquatable<Transference>
    {
        public bool Equals(Transference other)
        {
            return base.Equals(other);
        }
    }
}