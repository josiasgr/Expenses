using Entities.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Transactions
{
    public sealed class Expense : ExpenseEntity, IEquatable<Expense>
    {
        public new IEnumerable<TransactionDetails> TransactionDetails { get; set; }

        public Expense()
        {
            Id = Guid.NewGuid().ToString();
        }

        public bool Equals(Expense other)
        {
            return Id.Equals(other.Id, StringComparison.InvariantCulture)
                    && Name.Equals(other.Name, StringComparison.InvariantCulture)
                    && AccountId.Equals(other.AccountId, StringComparison.InvariantCulture)
                    && Date.Equals(other.Date)
                    && Sequence.Equals(other.Sequence)
                    && TransactionDetails.SequenceEqual(other.TransactionDetails);
        }
    }
}