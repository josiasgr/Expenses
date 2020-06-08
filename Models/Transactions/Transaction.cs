using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.Transactions
{
    public abstract class Transaction
    {
        public readonly Guid Id = new Guid();

        public readonly TransactionType TransactionType;
        public DateTime Date { get; set; } = DateTime.Now;
        private IEnumerable<TransactionDetails> TransactionDetails { get; } = Enumerable.Empty<TransactionDetails>();

        public float Total => TransactionDetails.Sum(s => s.Value);

        public Transaction(TransactionType transactionType)
        {
            TransactionType = transactionType;
        }
    }
}