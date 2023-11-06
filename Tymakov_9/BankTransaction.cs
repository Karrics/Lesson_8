using System;


namespace Tymakov_9
{
    internal class BankTransaction
    {
        public readonly DateTime TransactionDateTime;
        public readonly decimal TransactionAmount;

        public BankTransaction(decimal amount)
        {
            TransactionDateTime = DateTime.Now;
            TransactionAmount = amount;
        }
    }
}
