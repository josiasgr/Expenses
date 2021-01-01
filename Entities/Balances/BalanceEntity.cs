using System;

namespace Entities.Balance
{
    public abstract class BalanceEntity : Entity
    {
        public string AccountId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal InitialValue { get; set; }
        public decimal CurrentValue { get; set; }
    }
}