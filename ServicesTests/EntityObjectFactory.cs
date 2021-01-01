using Bogus;
using Entities;
using System.Collections;
using System.Collections.Generic;

namespace ServicesTests
{
    public class EntityObjectFactory<T> : IEnumerable<object[]> where T : Entity
    {
        public virtual Faker<T> Data
            => new Faker<T>()
                    .RuleFor(e => e.Id, f => f.Random.Guid().ToString());

        public IEnumerator<object[]> GetEnumerator()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new[] { Data.Generate() };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}