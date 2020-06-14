using Bogus;
using Domain.Accounts;
using Services;
using Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTests
{
    public class AccountServicesTests
    {
        private readonly IStorage _storage;

        /// <summary>
        /// Use on test cases as
        ///
        /// [Theory]
        /// [MemberData(nameof(AccountNames))]
        ///
        /// Returns random account names for xUnit tests, similar to:
        ///
        /// AccountNames =>
        /// new[] {
        ///     new [] { random string },
        ///     new [] { random string }
        /// }
        ///
        /// But on every read returns a dynamic result.
        /// </summary>
        public static IEnumerable<object[]> AccountNames =>
            new Faker()
                .Make(10, f => new Faker().Company.CompanyName())
                .Select(s => new[] { s })
                .ToArray();

        public AccountServicesTests()
        {
            _storage = new JsonFileStorage(@"C:\Test", "", true);
        }

        [Theory]
        [MemberData(nameof(AccountNames))]
        public async Task CreateAndReadAccountsByName(string accountName)
        {
            // Arrange
            var accountServices = new AccountServices(_storage);

            // Act
            var accountCreated = await accountServices.Create(accountName);

            // Assert
            var accountRead = await accountServices.Read(accountCreated.Id);
            Assert.Equal(accountCreated, accountRead);
        }

        [Theory]
        [ClassData(typeof(AccountObjectData))]
        public async Task CreateAndReadAccountsByObject(Account account)
        {
            // Arrange
            var accountServices = new AccountServices(_storage);

            // Act
            var accountCreated = await accountServices.Create(account);

            // Assert
            var accountRead = await accountServices.Read(accountCreated.Id);
            Assert.Equal(accountCreated, accountRead);
        }

        [Fact]
        public async Task ReadBy()
        {
            // Arrange
            var accountServices = new AccountServices(_storage);
            await accountServices.Create(new Account
            {
                Id = "1",
                Name = "ReadBy"
            }, true);

            // Act
            var enumerable = await accountServices.ReadBy(w => w.Name == "ReadBy");

            // Assert
            Assert.NotNull(enumerable.AsEnumerable().SingleOrDefault());
        }

        [Theory]
        [MemberData(nameof(AccountNames))]
        public async Task UpdateAccounts(string accountName)
        {
            // Arrange
            var accountServices = new AccountServices(_storage);
            var account = await accountServices.Create(accountName);

            // Act
            account.Name += account.Name;
            var upd = await accountServices.Update(account);

            // Assert
            Assert.NotNull(upd);
        }

        [Theory]
        [MemberData(nameof(AccountNames))]
        public async Task DeleteAccounts(string accountName)
        {
            // Arrange
            var accountServices = new AccountServices(_storage);
            var account = await accountServices.Create(accountName);

            // Act
            var wasDeleted = await accountServices.Delete(account);

            // Assert
            Assert.True(wasDeleted);
        }
    }
}