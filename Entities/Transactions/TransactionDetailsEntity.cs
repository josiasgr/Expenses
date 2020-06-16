using System.Collections.Generic;

namespace Entities.Transactions
{
    public abstract class TransactionDetailsEntity : Entity
    {
        public decimal Value { get; set; }
        public IDictionary<string, string> Tags { get; set; }
    }
}