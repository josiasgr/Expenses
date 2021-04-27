using Domain.Transactions;
using Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Transactions
{
    public class ExpenseServices : TransactionServices<Expense>
    {
        public ExpenseServices(
            IStorage[] storage
        ) : base(storage) { }

        public Task<Expense[]> Create(
            string accountId,
            DateTime dateTime,
            int sequence,
            IEnumerable<TransactionDetails> transactionDetails,
            bool overwriteIfExists = false
        )
        {
            var expense = new Expense
            {
                // Entity
                Id = GenerateId(accountId, dateTime),

                // Transaction Entity
                AccountId = accountId,
                Date = dateTime,
                Sequence = sequence,
                TransactionDetails = transactionDetails
            };

            return Create(expense, overwriteIfExists);
        }

        public Task<Expense> Read(string accountId, DateTime dateTime)
        {
            var id = GenerateId(accountId, dateTime);
            return Read(id);
        }

        private string GenerateId(string accountId, DateTime dateTime)
            => $"{accountId}-{dateTime:yyyy-MM-dd}-{Guid.NewGuid()}";
    }
}