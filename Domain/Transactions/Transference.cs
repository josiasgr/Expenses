using Entities.Transactions;
using System;

namespace Domain.Transactions
{
    public class Transference : TransferenceEntity
    {
        public Transference()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}