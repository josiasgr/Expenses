using Bogus;
using Domain.Accounts;
using System.Collections;
using System.Collections.Generic;

namespace ServicesTests
{
    /// <summary>
    /// Use on test cases as
    /// [Theory]
    /// [MemberData(nameof(AccountNames))]
    /// </summary>
    public class AccountObjectData : IEnumerable<object[]>
    {
        private readonly Faker<Account>
            data = new Faker<Account>()
                    .RuleFor(acc => acc.Name, f => f.Company.CompanyName());

        public IEnumerator<object[]> GetEnumerator()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new[] { data.Generate() };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}