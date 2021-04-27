using Entities.Accounts;
using System;

namespace Domain.Accounts
{
    public sealed class Account : AccountEntity, IEquatable<Account>
    {
        public bool Equals(Account other)
        {
            return string.Compare(Id, other?.Id, StringComparison.InvariantCultureIgnoreCase) == 0
                    && string.Compare(Name, other?.Name, StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }
}