using Bogus;
using Domain.Accounts;
using Domain.Balances;
using Services.Accounts;
using Services.Balances;
using Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTests.Balances
{
    public class MonthlyBalanceServicesTests
    {
        private readonly string _storageBaseFolder = @"C:\Test";
        private Account _account;

        /// <summary>
        /// Use on test cases as
        ///
        /// [Theory]
        /// [MemberData(nameof(BalanceDates))]
        ///
        /// Returns random Balance names for xUnit tests, similar to:
        ///
        /// BalanceDates =>
        /// new[] {
        ///     new [] { random string },
        ///     new [] { random string }
        /// }
        ///
        /// But on every read returns a dynamic result.
        /// </summary>
        public static IEnumerable<object[]> BalanceDates =>
            new[] {
                new [] { "2020-01-01" },
                new [] { "2020-02-01" },
                new [] { "2020-03-01" },
                new [] { "2020-04-01" },
                new [] { "2020-05-01" },
                new [] { "2020-06-01" },
                new [] { "2020-07-01" },
                new [] { "2020-08-01" },
                new [] { "2020-09-01" },
                new [] { "2020-10-01" },
                new [] { "2020-11-01" },
                new [] { "2020-12-01" }
            };

        public MonthlyBalanceServicesTests()
        {
            Task.Run(async () =>
            {
                _account = await new AccountServices(new JsonFileStorage(_storageBaseFolder, "", true))
                    .Create("TransactionServicesTests", true);
            }).Wait();
        }

        [Theory]
        [MemberData(nameof(BalanceDates))]
        public async Task CreateAndReadBalancesByDate(string strDate)
        {
            // Arrange
            var date = DateTime.Parse(strDate);
            var services = new MonthlyBalanceServices(
                _account.Id,
                GetStorageForBalanceDate(date)
            );

            // Act
            var balanceCreated = await services.Create(date, true);

            // Assert
            var balanceRead = await services.Read(balanceCreated.Id);
            Assert.Equal(balanceCreated, balanceRead);
        }

        [Theory]
        [ClassData(typeof(MonthlyBalanceObjectData))]
        public async Task CreateAndReadBalancesByObject(MonthlyBalance balance)
        {
            // Arrange
            balance.AccountId = _account.Id;
            var services = new MonthlyBalanceServices(
                _account.Id,
                GetStorageForBalanceDate(balance.Date)
            );

            // Act
            var balanceCreated = await services.Create(balance);

            // Assert
            var balanceRead = await services.Read(balanceCreated.Id);
            Assert.Equal(balanceCreated, balanceRead);
        }

        [Theory]
        [MemberData(nameof(BalanceDates))]
        public async Task ReadBy(string strDate)
        {
            // Arrange
            var date = DateTime.Parse(strDate);
            var services = new MonthlyBalanceServices(
                _account.Id,
                GetStorageForBalanceDate(date)
            );

            // Act
            var enumerable = await services.ReadBy(w => w.Date == date);

            // Assert
            Assert.NotNull(enumerable.AsEnumerable().SingleOrDefault());
        }

        [Theory]
        [MemberData(nameof(BalanceDates))]
        public async Task UpdateAccounts(string strDate)
        {
            // Arrange
            var date = DateTime.Parse(strDate).AddYears(1);
            var services = new MonthlyBalanceServices(
                _account.Id,
                GetStorageForBalanceDate(date)
            );
            var balanceCreated = await services.Create(date);

            // Act
            balanceCreated.Name += balanceCreated.Name;
            var upd = await services.Update(balanceCreated);

            // Assert
            Assert.NotNull(upd);
        }

        [Theory]
        [MemberData(nameof(BalanceDates))]
        public async Task DeleteAccounts(string strDate)
        {
            // Arrange
            var date = DateTime.Parse(strDate);
            var services = new MonthlyBalanceServices(
                _account.Id,
                GetStorageForBalanceDate(date)
            );

            // Act
            var wasDeleted = await services.Delete(date);

            // Assert
            Assert.True(wasDeleted);
        }

        private IStorage GetStorageForBalanceDate(DateTime date)
        {
            return new JsonFileStorage(
                    _storageBaseFolder,
                    Path.Combine(
                        $"{date:yyyy}",
                        $"{date:MM}",
                        $"{date:dd}"
                    ),
                    true
                );
        }
    }
}