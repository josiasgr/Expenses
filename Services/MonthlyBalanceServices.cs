using Domain.Accounts;
using Domain.Balances;
using Storage;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class MonthlyBalanceServices : BalanceServices<MonthlyBalance>
    {
        public MonthlyBalanceServices(
            IStorage storage
        ) : base(storage) { }

        public Task<MonthlyBalance> Create(Account account, DateTime dateTime, bool overwriteIfExists = false)
        {
            var balance = new MonthlyBalance
            {
                Id = $"{account.Id}-{dateTime:yyyy-MM}",
                Name = $"{dateTime:MMMM, yyyy}",
                AccountId = account.Id,
                Date = new DateTime(dateTime.Year, dateTime.Month, 01)
            };

            return Create(balance, overwriteIfExists);
        }

        public Task<MonthlyBalance> Read(Account account, DateTime dateTime)
        {
            var id = $"{account.Id}-{dateTime.Year}-{dateTime.Month}";
            return Read(id);
        }
    }
}