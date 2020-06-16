using Entities.Balance;
using System;

namespace Domain.Balances
{
    public sealed class MonthlyBalance : MonthlyBalanceEntity, IEquatable<MonthlyBalance>
    {
        public MonthlyBalance()
        {
            Id = Guid.NewGuid().ToString();
        }

        public bool Equals(MonthlyBalance other)
        {
            return Id.Equals(other.Id, StringComparison.InvariantCulture)
                    && Name.Equals(other.Name, StringComparison.InvariantCulture)
                    && AccountId.Equals(other.AccountId, StringComparison.InvariantCulture)
                    && Date.Equals(other.Date)
                    && InitialValue.Equals(other.InitialValue)
                    && CurrentValue.Equals(other.CurrentValue);
        }
    }
}