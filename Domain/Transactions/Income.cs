using Entities.Transactions;
using System;

namespace Domain.Transactions
{
    public class Income : IncomeEntity
    {
        public Income()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}