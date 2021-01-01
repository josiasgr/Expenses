using Storage;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Xunit;

namespace StorageTests
{
    public class DummyClass : IEquatable<DummyClass>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public bool Equals([AllowNull] DummyClass other)
        {
            return this.Id.Equals(other.Id, StringComparison.InvariantCultureIgnoreCase)
                  && this.Timestamp == other.Timestamp;
        }
    }

    public class StorageTests
    {
        private JsonFileStorage GetRandomStorage(bool enableVersionControl)
            => new JsonFileStorage(Path.GetTempFileName().Split('.')[0], enableVersionControl);

        [Fact]
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

        [Fact]
        public async void CreateFileOnStorage()
        {
            // Arrange
            var storage = GetRandomStorage(false);
            var storageWithVersionControl = GetRandomStorage(true);
            var o = new DummyClass();

            // Act
            var created1 = await storage.Create(o);
            var created2 = await storageWithVersionControl.Create(o);

            // Assert
            Assert.Equal(created1, o);
            Assert.Equal(created2, o);

            var h = storageWithVersionControl.History(created2);
        }
    }
}