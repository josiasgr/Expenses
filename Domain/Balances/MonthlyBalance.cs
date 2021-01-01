using Entities.Balance;
using System;

namespace Domain.Balances
{
    public sealed class MonthlyBalance : BalanceEntity, IEquatable<MonthlyBalance>
    {
        public bool Equals(MonthlyBalance other)
        {
            return Id.Equals(other.Id, StringComparison.InvariantCultureIgnoreCase)
                    && AccountId.Equals(other.AccountId, StringComparison.InvariantCultureIgnoreCase)
                    && FromDate.Equals(other.FromDate)
                    && ToDate.Equals(other.ToDate)
                    && InitialValue.Equals(other.InitialValue)
                    && CurrentValue.Equals(other.CurrentValue);
        }
    }
}