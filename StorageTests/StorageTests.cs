using Storage;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace StorageTests
{
    public record DummyClass(string Id, DateTime Timestamp);

    public class StorageTests
    {
        private static IStorage GetRandomStorage(bool enableVersionControl)
            => new JsonFileStorage(
                new JsonFileStorageConfig(
                        Path.Combine(Path.GetTempPath(), DateTime.Now.ToShortDateString(), enableVersionControl.ToString()),
                        enableVersionControl
                    )
            );

        [Fact, Order(2)]
        public void CreateJsonFileStorage()
        {
            // Arrange

            // Act
            var storage = GetRandomStorage(false);
            var storageWithVersionControl = GetRandomStorage(true);

            // Assert
            Assert.True(storage is JsonFileStorage);
            Assert.True(storageWithVersionControl is JsonFileStorage);
        }

        [Fact, Order(2)]
        public async Task CreateFileOnStorage()
        {
            // Arrange
            var storage = GetRandomStorage(false);
            var storageWithVersionControl = GetRandomStorage(true);
            var o = new DummyClass(Guid.NewGuid().ToString(), DateTime.Now);

            // Act
            var created1 = await storage.CreateAsync(o);
            var created2 = await storageWithVersionControl.CreateAsync(o);

            // Assert
            Assert.Equal(o, created1);
            Assert.Equal(o, created2);
        }

        [Fact, Order(3)]
        public async Task ReadFileOnStorageById()
        {
            // Arrange
            var storage = GetRandomStorage(false);
            var storageWithVersionControl = GetRandomStorage(true);

            // Act
            var read1 = await storage.ReadAsync<DummyClass>("1");
            var read2 = await storageWithVersionControl.ReadAsync<DummyClass>("1");

            // Assert
            Assert.Equal("1", read1.Id);
            Assert.Equal("1", read2.Id);
        }

        [Fact, Order(4)]
        public async Task ReadFileOnStorageByPredicate()
        {
            // Arrange
            var storage = GetRandomStorage(false);
            var storageWithVersionControl = GetRandomStorage(true);
            static bool findId1(DummyClass p) => p.Id.Equals("1", StringComparison.InvariantCultureIgnoreCase);

            // Act
            var read1 = (
                            await storage.ReadByAsync<DummyClass>(findId1)
                        ).SingleOrDefault();
            var read2 = (
                            await storageWithVersionControl.ReadByAsync<DummyClass>(findId1)
                        ).SingleOrDefault();

            // Assert
            Assert.Equal("1", read1.Id);
            Assert.Equal("1", read2.Id);
        }
    }
}