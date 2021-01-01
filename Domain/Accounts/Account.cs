using Entities.Accounts;
using System;

namespace Domain.Accounts
{
    public sealed class Account : AccountEntity, IEquatable<Account>
    {
        public bool Equals(Account other)
        {
            return Id.Equals(other.Id, StringComparison.InvariantCultureIgnoreCase)
                    && Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}