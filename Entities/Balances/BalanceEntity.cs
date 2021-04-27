using System;

namespace Entities.Balance
{
    /// <summary>
    /// Balances are like "calculated views" of all transactions between From and To date...
    /// </summary>
    public abstract class BalanceEntity : Entity
    {
        public string AccountId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal InitialValue { get; set; }
        public decimal CurrentValue { get; }
    }
}