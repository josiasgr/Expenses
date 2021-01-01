using Bogus;
using Domain.Accounts;
using Domain.Tags;
using Domain.Transactions;
using Services.Accounts;
using Services.Transactions;
using Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTests.Transactions
{
    public class TransactionServicesTests
    {
        private Account _account;

        /// <summary>
        /// Use on test cases as
        ///
        /// [Theory]
        /// [MemberData(nameof(TransactionNames))]
        ///
        /// Returns random Transaction names for xUnit tests, similar to:
        ///
        /// TransactionNames =>
        /// new[] {
        ///     new [] { random string },
        ///     new [] { random string }
        /// }
        ///
        /// But on every read returns a dynamic result.
        /// </summary>
        public static IEnumerable<object[]> TransactionNames =>
            new Faker()
                .Make(10, f => new Faker().Company.CompanyName())
                .Select(s => new[] { s })
                .ToArray();

        public static IEnumerable<object[]> TransactionDates =>
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
        [MemberData(nameof(TransactionDates))]
        public async Task CreateAndReadExpences(string strDate)
        {
            _account = await new AccountServices(new JsonFileStorage(@"C:\Test", true))
                .Create("TransactionServicesTests", true);

            // Arrange
            var date = DateTime.Parse(strDate);
            var services = new ExpenseServices(
                new JsonFileStorage(
                    @"C:\Test"
                    , true
                )
            );

            // Act
            var expenseCreated = await services.Create(_account.Id, date, 0, new[] {
                new TransactionDetails {
                    Value = 1,
                    Tags= new List<string>()
                    {
                        "Despensa",
                        "SuperStore"
                    }
                }
            }, true);

            // Assert
            var expenseRead = await services.Read(expenseCreated.Id);
            Assert.Equal(expenseCreated, expenseRead);
        }

        //[Theory]
        //[ClassData(typeof(TransactionObjectData))]
        //public async Task CreateAndReadTransactionsByObject(Transaction Transaction)
        //{
        //    // Arrange
        //    var services = new TransactionServices(_storage);

        //    // Act
        //    var balanceCreated = await services.Create(Transaction);

        //    // Assert
        //    var TransactionRead = await services.Read(TransactionCreated.Id);
        //    Assert.Equal(TransactionCreated, TransactionRead);
        //}

        //[Fact]
        //public async Task ReadBy()
        //{
        //    // Arrange
        //    var services = new TransactionServices(_storage);
        //    await services.Create(new Transaction
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
        //[MemberData(nameof(TransactionNames))]
        //public async Task UpdateTransactions(string TransactionName)
        //{
        //    // Arrange
        //    var services = new TransactionServices(_storage);
        //    var Transaction = await services.Create(TransactionName);

        //    // Act
        //    Transaction.Name += Transaction.Name;
        //    var upd = await services.Update(Transaction);

        //    // Assert
        //    Assert.NotNull(upd);
        //}

        //[Theory]
        //[MemberData(nameof(TransactionNames))]
        //public async Task DeleteTransactions(string TransactionName)
        //{
        //    // Arrange
        //    var services = new TransactionServices(_storage);
        //    var Transaction = await services.Create(TransactionName);

        //    // Act
        //    var wasDeleted = await services.Delete(Transaction);

        //    // Assert
        //    Assert.True(wasDeleted);
        //}
    }
}