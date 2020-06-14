using Bogus;
using Domain.Accounts;
using Services;
using Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTests
{
    public class BalanceServicesTests
    {
        private readonly Account _account = new Account { Id = "1" };

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

        public BalanceServicesTests()
        {
        }

        [Theory]
        [MemberData(nameof(BalanceDates))]
        public async Task CreateAndReadBalancesByName(string strDate)
        {
            // Arrange
            var date = DateTime.Parse(strDate);
            var services = new MonthlyBalanceServices(
                new JsonFileStorage(@"C:\Test", $"{date.Year}", true)
            );

            // Act
            var balanceCreated = await services.Create(_account, date, true);

            // Assert
            var balanceRead = await services.Read(balanceCreated.Id);
            Assert.Equal(balanceCreated, balanceRead);
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