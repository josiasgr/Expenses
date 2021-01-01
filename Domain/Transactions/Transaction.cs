using Entities.Transactions;
using System;
using System.Collections.Generic;

namespace Domain.Transactions
{
    public class Transaction : TransactionEntity, IEquatable<Transaction>
    {
        public Transaction()
        {
            TransactionDetails = new List<TransactionDetails>();
        }

        public bool Equals(Transaction other)
        {
            return Id.Equals(other.Id, StringComparison.InvariantCultureIgnoreCase)
                    && AccountId.Equals(other.AccountId, StringComparison.InvariantCultureIgnoreCase)
                    && Date.Equals(other.Date)
                    && Sequence.Equals(other.Sequence)
                    && TransactionDetails.Equals(other.TransactionDetails);
        }
    }
}