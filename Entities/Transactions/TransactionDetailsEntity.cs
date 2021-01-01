using Entities.Tags;
using System.Collections.Generic;

namespace Entities.Transactions
{
    public abstract class TransactionDetailsEntity : Entity
    {
        public decimal Value { get; set; }
        public ICollection<string> Tags { get; set; }
    }
}