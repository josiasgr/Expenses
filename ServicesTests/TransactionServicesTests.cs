using Bogus;
using Domain.Accounts;
using Domain.Transactions;
using Services;
using Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTests
{
    public class TransactionServicesTests
    {
        private Account _account;

        /// <summary>
        /// Use on test cases as
        ///
        /// [Theory]
        /// [MemberData(nameof(BalanceNames))]
        ///
        /// Returns random Balance names for xUnit tests, similar to:
        ///
        /// BalanceNames =>
        /// new[] {
        ///     new [] { random string },
        ///     new [] { random string }
        /// }
        ///
        /// But on every read returns a dynamic result.
        /// </summary>
        public static IEnumerable<object[]> BalanceNames =>
            new Faker()
                .Make(10, f => new Faker().Company.CompanyName())
                .Select(s => new[] { s })
                .ToArray();

        public static IEnumerable<object[]> BalanceDates =>
            new[] {
                new [] { "2020-06-01" },
                new [] { "2020-06-02" },
                new [] { "2020-06-03" },
                new [] { "2020-06-04" },
                new [] { "2020-06-05" },
                new [] { "2020-06-06" },
                new [] { "2020-06-07" },
                new [] { "2020-06-08" },
                new [] { "2020-06-09" },
                new [] { "2020-06-10" },
                new [] { "2020-06-10" },
                new [] { "2020-06-10" }
            };

        [Theory]
        [MemberData(nameof(BalanceDates))]
        public async Task CreateAndReadExpences(string strDate)
        {
            _account = await new AccountServices(new JsonFileStorage(@"C:\Test", "", true))
                .Create("TransactionServicesTests", true);

            // Arrange
            var date = DateTime.Parse(strDate);
            var services = new ExpenseServices(
                new JsonFileStorage(
                    @"C:\Test",
                    Path.Combine(
                        $"{date:yyyy}",
                        $"{date:MM}",
                        $"{date:dd}"
                    )
                    , true
                )
            );

            // Act
            var expenseCreated = await services.Create(_account.Id, date, 0, new[] {
                new TransactionDetails {
                    Tags=new Dictionary<string, string>
                    {
                        { "Despensa", "SuperStore" }
                    }
                }
            }, true);

            // Assert
            var expenseRead = await services.Read(expenseCreated.Id);
            Assert.Equal(expenseCreated, expenseRead);
        }

        //[Theory]
        //[ClassData(typeof(BalanceObjectData))]
        //public async Task CreateAndReadBalancesByObject(Balance Balance)
        //{
        //    // Arrange
        //    var services = new BalanceServices(_storage);

        //    // Act
        //    var balanceCreated = await services.Create(Balance);

        //    // Assert
        //    var BalanceRead = await services.Read(BalanceCreated.Id);
        //    Assert.Equal(BalanceCreated, BalanceRead);
        //}

        //[Fact]
        //public async Task ReadBy()
        //{
        //    // Arrange
        //    var services = new BalanceServices(_storage);
        //    await services.Create(new Balance
        //    {
        //        Id = "1",
        //        Name = "ReadBy"
        //    }, true);

        //    // Act
        //    var enumerable = await services.ReadBy(w => w.Name == "ReadBy");

        //    // Assert
        //    Assert.NotNull(enumerable.AsEnumerable().SingleOrDefault());
        //}

        //[Theory]
        //[MemberData(nameof(BalanceNames))]
        //public async Task UpdateBalances(string BalanceName)
        //{
        //    // Arrange
        //    var services = new BalanceServices(_storage);
        //    var Balance = await services.Create(BalanceName);

        //    // Act
        //    Balance.Name += Balance.Name;
        //    var upd = await services.Update(Balance);

        //    // Assert
        //    Assert.NotNull(upd);
        //}

        //[Theory]
        //[MemberData(nameof(BalanceNames))]
        //public async Task DeleteBalances(string BalanceName)
        //{
        //    // Arrange
        //    var services = new BalanceServices(_storage);
        //    var Balance = await services.Create(BalanceName);

        //    // Act
        //    var wasDeleted = await services.Delete(Balance);

        //    // Assert
        //    Assert.True(wasDeleted);
        //}
    }
}