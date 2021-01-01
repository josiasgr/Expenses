namespace Entities.Tags
{
    public class TagEntity : Entity
    {
        public string Value { get; set; }
        public TagEntity Parent { get; set; }
    }
}