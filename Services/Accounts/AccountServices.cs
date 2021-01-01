using Domain.Accounts;
using Storage;
using System.Threading.Tasks;

namespace Services.Accounts
{
    public class AccountServices : Services<Account>
    {
        public AccountServices(
            IStorage storage
        ) : base(storage) { }

        public Task<Account> Create(
            string accountName,
            bool overwriteIfExists = false
        )
        {
            return Create(new Account
            {
                Name = accountName
            }, overwriteIfExists);
        }

        public Task<bool> Delete(Account account)
        {
            return Delete(account.Id);
        }
    }
}