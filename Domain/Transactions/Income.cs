using System;

namespace Domain.Transactions
{
    public sealed class Income : Transaction, IEquatable<Income>
    {
        public bool Equals(Income other)
        {
            return base.Equals(other);
        }
    }
}