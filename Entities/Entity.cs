using System;

namespace Entities
{
    /// <summary>
    /// Entities are meant to be plain objects for persistence
    /// </summary>
    public abstract class Entity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}