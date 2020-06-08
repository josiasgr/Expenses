using Models.Balances;
using System;
using System.Collections.Generic;

namespace Models
{
    internal class Account
    {
        public readonly Guid Id;

        public string Name { get; set; }

        public IEnumerable<IBalance> Balances;
    }
}