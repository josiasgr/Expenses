using Entities.Transactions;
using System;
using System.Linq;

namespace Domain.Transactions
{
    public class TransactionDetails : TransactionDetailsEntity, IEquatable<TransactionDetailsEntity>
    {
        public TransactionDetails()
        {
            Tags = Enumerable.Empty<string>();
        }

        public bool Equals(TransactionDetailsEntity other)
        {
            return string.Compare(Id, other?.Id, StringComparison.InvariantCultureIgnoreCase) == 0
                    && Value == other?.Value
                    && Tags.Equals(other?.Tags);
        }
    }
}