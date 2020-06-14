using Domain.Accounts;
using Storage;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class AccountServices : Services<Account>
    {
        public AccountServices(
            IStorage storage
        ) : base(storage) { }

        public Task<Account> Create(string accountName, bool overwriteIfExists = false)
        {
            return Create(new Account
            {
                Id = Guid.NewGuid().ToString(),
                Name = accountName
            }, overwriteIfExists);
        }

        public Task<bool> Delete(Account account)
        {
            return Delete(account.Id);
        }
    }
}