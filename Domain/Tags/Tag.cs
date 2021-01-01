using Entities.Tags;
using System;

namespace Domain.Tags
{
    public class Tag : TagEntity, IEquatable<Tag>
    {
        public Tag(string value)
        {
            Value = value;
        }

        public bool Equals(Tag other)
        {
            return string.Compare(base.Value, other.Value, StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }
}