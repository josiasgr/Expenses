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
                    .RuleFor(
                        b => b.FromDate,
                        f => f.Date.Between(DateTime.Parse("2021-01-01"), DateTime.Today)
                    );
    }
}