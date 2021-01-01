using System;

namespace Domain.Transactions
{
    public sealed class Expense : Transaction, IEquatable<Expense>
    {
        public bool Equals(Expense other)
        {
            return base.Equals(other);
        }
    }
}