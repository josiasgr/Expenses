using System;
using System.Collections.Generic;

namespace Models.Transactions
{
    public abstract class TransactionDetails
    {
        public readonly Guid Id = new Guid();
        public float Value { get; set; }
        public Dictionary<string, string> Tags { get; set; }
    }
}