using Storage;
using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Extensions.Ordering;

namespace StorageTests
{
    public class DummyClass : IEquatable<DummyClass>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public bool Equals(DummyClass other)
        {
            return this.Id.Equals(other?.Id, StringComparison.InvariantCultureIgnoreCase)
                  && this.Timestamp == other?.Timestamp;
        }
    }

    public class StorageTests
    {
        private IStorage GetRandomStorage(bool enableVersionControl)
            => new JsonFileStorage(new JsonFileStorageConfig
            {
                Entity = "",
                EnableVersionControl = enableVersionControl,
                StorageFolder = Path.Combine(Path.GetTempPath(), DateTime.Now.ToShortDateString(), enableVersionControl.ToString())
            });

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
        public void CreateFileOnStorage()
        {
            // Arrange
            var storage = GetRandomStorage(false);
            var storageWithVersionControl = GetRandomStorage(true);
            var o = new DummyClass
            {
                Id = "1"
            };

            // Act
            var created1 = storage.Create(o).GetAwaiter().GetResult();
            var created2 = storageWithVersionControl.Create(o).GetAwaiter().GetResult();

            // Assert
            Assert.Equal(o, created1);
            Assert.Equal(o, created2);
        }

        [Fact, Order(3)]
        public void ReadFileOnStorageById()
        {
            // Arrange
            var storage = GetRandomStorage(false);
            var storageWithVersionControl = GetRandomStorage(true);

            // Act
            var read1 = storage.Read<DummyClass>("1").GetAwaiter().GetResult();
            var read2 = storageWithVersionControl.Read<DummyClass>("1").GetAwaiter().GetResult();

            // Assert
            Assert.Equal("1", read1.Id);
            Assert.Equal("1", read2.Id);
        }

        [Fact, Order(4)]
        public void ReadFileOnStorageByPredicate()
        {
            // Arrange
            var storage = GetRandomStorage(false);
            var storageWithVersionControl = GetRandomStorage(true);

            // Act
            var read1 = storage
                        .ReadBy<DummyClass>(p => p.Id.Equals("1", StringComparison.InvariantCultureIgnoreCase))
                        .GetAwaiter().GetResult()
                        .FirstOrDefault();
            var read2 = storageWithVersionControl
                        .ReadBy<DummyClass>(p => p.Id.Equals("1", StringComparison.InvariantCultureIgnoreCase))
                        .GetAwaiter().GetResult()
                        .FirstOrDefault();

            // Assert
            Assert.Equal("1", read1.Id);
            Assert.Equal("1", read2.Id);
        }
    }
}