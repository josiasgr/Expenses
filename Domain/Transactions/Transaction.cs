using Entities.Transactions;
using System;
using System.Linq;

namespace Domain.Transactions
{
    public class Transaction : TransactionEntity, IEquatable<Transaction>
    {
        public Transaction()
        {
            TransactionDetails = Enumerable.Empty<TransactionDetailsEntity>();
        }

        public bool Equals(Transaction other)
        {
            return string.Compare(Id, other?.Id, StringComparison.InvariantCultureIgnoreCase) == 0
                    && string.Compare(AccountId, other?.AccountId, StringComparison.InvariantCultureIgnoreCase) == 0
                    && Date == other?.Date
                    && Sequence == other?.Sequence
                    && TransactionDetails.Equals(other?.TransactionDetails);
        }
    }
}