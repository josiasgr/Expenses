using Entities.Transactions;
using System;

namespace Domain.Transactions
{
    public class TransactionDetails : TransactionDetailsEntity, IEquatable<TransactionDetailsEntity>
    {
        public bool Equals(TransactionDetailsEntity other)
        {
            return Id.Equals(other.Id, StringComparison.InvariantCultureIgnoreCase)
                    && Value.Equals(other.Value)
                    && Tags.Equals(other.Tags);
        }
    }
}