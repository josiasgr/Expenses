using Bogus;
using Domain.Accounts;
using Services.Accounts;
using Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTests.Accounts
{
    public class AccountServicesTests
    {
        private readonly string _storageBaseFolder = @"C:\Test";
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
            => _storage = new JsonFileStorage(new JsonFileStorageConfig { 
                Entity= "Account",
                StorageFolder = _storageBaseFolder,
                EnableVersionControl = true
            });

        [Theory]
        [MemberData(nameof(AccountNames))]
        public async Task CreateAndReadAccountsByName(string accountName)
        {
            // Arrange
            var services = new AccountServices(new[] { _storage } );

            // Act
            var accountCreated = (await services.Create(accountName)).FirstOrDefault();
            

            // Assert
            var accountRead = await services.Read(accountCreated.Id);
            Assert.Equal(accountCreated, accountRead);
        }

        [Theory]
        [ClassData(typeof(AccountObjectData))]
        public async Task CreateAndReadAccountsByObject(Account account)
        {
            // Arrange
            var services = new AccountServices(new[] { _storage });

            // Act
            await services.Create(account);
            var accountCreated = await services.Read(account.Id);

            // Assert
            var accountRead = await services.Read(accountCreated.Id);
            Assert.Equal(accountCreated, accountRead);
        }

        [Theory]
        [MemberData(nameof(AccountNames))]
        public async Task ReadBy(string accountName)
        {
            // Arrange
            var services = new AccountServices(new[] { _storage });
            await services.Create(accountName);

            // Act
            var enumerable = await services.ReadBy(w => w.Name == accountName);

            // Assert
            Assert.NotNull(enumerable.AsEnumerable().SingleOrDefault());
        }

        [Theory]
        [MemberData(nameof(AccountNames))]
        public async Task UpdateAccounts(string accountName)
        {
            // Arrange
            var services = new AccountServices(new[] { _storage });
            await services.Create(accountName);
            var accountCreated = await services.Read(accountName);

            // Act
            accountCreated.Name += accountCreated.Name;
            var upd = await services.Update(accountCreated);

            // Assert
            Assert.NotNull(upd);
        }

        [Theory]
        [MemberData(nameof(AccountNames))]
        public async Task DeleteAccounts(string accountName)
        {
            // Arrange
            var services = new AccountServices(new[] { _storage });
            var accountCreated = (await services.Create(accountName)).FirstOrDefault();

            // Act
            var wasDeleted = (await services.Delete(accountCreated)).FirstOrDefault();

            // Assert
            Assert.True(wasDeleted);
        }
    }
}