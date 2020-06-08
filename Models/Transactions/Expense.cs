namespace Models.Transactions
{
    public class Expense : Transaction
    {
        public Expense() : base(TransactionType.Expense)
        {
        }
    }
}