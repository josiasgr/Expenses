namespace Entities
{
    /// <summary>
    /// Entities are meant to be POCO for persistence
    /// Everything must inherit from Entity
    /// </summary>
    public class Entity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}