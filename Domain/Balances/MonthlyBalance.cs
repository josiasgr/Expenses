using Entities.Balance;
using System;

namespace Domain.Balances
{
    public sealed class MonthlyBalance : BalanceEntity, IEquatable<MonthlyBalance>
    {
        public bool Equals(MonthlyBalance other)
        {
            return string.Compare(Id, other?.Id, StringComparison.InvariantCultureIgnoreCase) == 0
                    && string.Compare(AccountId, other?.AccountId, StringComparison.InvariantCultureIgnoreCase) == 0
                    && FromDate == other?.FromDate
                    && ToDate == other?.ToDate
                    && InitialValue == other?.InitialValue
                    && CurrentValue == other?.CurrentValue;
        }

        public override string ToString()
        {
            return $"{FromDate:yyyy-MM}";
        }
    }
}