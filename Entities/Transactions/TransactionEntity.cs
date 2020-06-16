using System;
using System.Collections.Generic;

namespace Entities.Transactions
{
    public abstract class TransactionEntity : Entity
    {
        public string AccountId { get; set; }
        public DateTime Date { get; set; }
        public int Sequence { get; set; }
        public IEnumerable<TransactionDetailsEntity> TransactionDetails { get; set; }
    }
}