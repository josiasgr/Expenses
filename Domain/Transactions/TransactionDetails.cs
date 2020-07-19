using Entities.Transactions;
using System;
using System.Linq;

namespace Domain.Transactions
{
    public sealed class TransactionDetails : TransactionDetailsEntity, IEquatable<TransactionDetails>
    {
        public TransactionDetails()
        {
            Id = Guid.NewGuid().ToString();
        }

        public bool Equals(TransactionDetails other)
        {
            return Id.Equals(other.Id, StringComparison.InvariantCulture)
                    && Name.Equals(other.Name, StringComparison.InvariantCulture)
                    && Value.Equals(other.Value)
                    && Tags.SequenceEqual(other.Tags);
        }
    }
}