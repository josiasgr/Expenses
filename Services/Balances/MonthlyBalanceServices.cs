using Domain.Balances;
using Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Balances
{
    public class MonthlyBalanceServices : BalanceServices<MonthlyBalance>
    {
        private readonly string _accountId;

        public MonthlyBalanceServices(
            string accountId,
            IStorage[] storage
        ) : base(storage)
        {
            _accountId = accountId;
        }

        public Task<MonthlyBalance[]> Create(DateTime dateTime, bool overwriteIfExists = false)
        {
            var balance = new MonthlyBalance
            {
                Id = GenerateId(dateTime),
                AccountId = _accountId,
                FromDate = new DateTime(dateTime.Year, dateTime.Month, 01),
                ToDate = new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month))
            };

            return Create(balance, overwriteIfExists);
        }

        public Task<MonthlyBalance> Read(DateTime dateTime)
        {
            var id = GenerateId(dateTime);
            return Read(id);
        }

        public override Task<IEnumerable<MonthlyBalance>> ReadBy(Func<MonthlyBalance, bool> predicate)
        {
            bool injectAccountId(MonthlyBalance o)
                => o.AccountId == _accountId && predicate(o);

            return base.ReadBy(injectAccountId);
        }

        public Task<bool[]> Delete(DateTime dateTime)
        {
            var id = GenerateId(dateTime);
            return Delete(id);
        }

        private string GenerateId(DateTime dateTime)
            => $"{_accountId}-{dateTime:yyyy-MM}";
    }
}