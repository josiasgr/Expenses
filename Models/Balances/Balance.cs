using Models.Balances;
using System;

namespace Models.Balance
{
    public abstract class Balance : IBalance
    {
        public readonly BalanceType BalanceType;

        public DateTime Date;

        public float Value;

        public Balance(BalanceType balanceType)
        {
            BalanceType = balanceType;
        }
    }
}