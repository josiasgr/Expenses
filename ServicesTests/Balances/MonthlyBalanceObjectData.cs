using Bogus;
using Domain.Balances;
using System;

namespace ServicesTests.Balances
{
    /// <summary>
    /// Use on test cases as
    /// [Theory]
    /// [ClassData(typeof(MonthlyBalanceObjectData))]
    /// </summary>
    public class MonthlyBalanceObjectData : EntityObjectFactory<MonthlyBalance>
    {
        public override Faker<MonthlyBalance> Data
            => new Faker<MonthlyBalance>()
                    .RuleFor(b => b.Date, f => f.Date.Between(DateTime.Parse("2019-01-01"), DateTime.Parse("2019-12-31")))
                    .RuleFor(acc => acc.Name, f => f.Company.CompanyName());
    }
}