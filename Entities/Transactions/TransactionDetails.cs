using System.Collections.Generic;

namespace Entities.Transactions
{
    public abstract class TransactionDetails : Entity
    {
        public decimal Value { get; set; }
        public Dictionary<string, string> Tags { get; set; }
    }
}