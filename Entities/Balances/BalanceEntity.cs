using System;

namespace Entities.Balance
{
    public abstract class BalanceEntity : Entity
    {
        public string AccountId { get; set; }
        public DateTime Date { get; set; }
        public decimal InitialValue { get; set; }
        public decimal CurrentValue { get; set; }
    }
}