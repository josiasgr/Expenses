using Domain.Accounts;

namespace ServicesTests.Accounts
{
    /// <summary>
    /// Use on test cases as
    /// [Theory]
    /// [ClassData(typeof(AccountObjectData)]
    /// </summary>
    public class AccountObjectData : EntityObjectFactory<Account>
    {
    }
}