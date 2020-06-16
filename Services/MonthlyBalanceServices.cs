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

        public Task<MonthlyBalance> Create(string accountId, DateTime dateTime, bool overwriteIfExists = false)
        {
            var balance = new MonthlyBalance
            {
                Id = GenerateId(accountId, dateTime),
                Name = $"{dateTime:MMMM, yyyy}",
                AccountId = accountId,
                Date = new DateTime(dateTime.Year, dateTime.Month, 01)
            };

            return Create(balance, overwriteIfExists);
        }

        public Task<MonthlyBalance> Read(string accountId, DateTime dateTime)
        {
            var id = GenerateId(accountId, dateTime);
            return Read(id);
        }

        private string GenerateId(string accountId, DateTime dateTime)
            => $"{accountId}-{dateTime:yyyy-MM}";
    }
}