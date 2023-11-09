using System;
using System.Collections.Generic;
using System.IO;

namespace Tymakov_9
{
    internal class BankAccount 
    {
        private static int nextAccountNumber = 1;
        private int accountNumber;
        private decimal balance;
        private BankAccountType accountType;
        private Queue<BankTransaction> transactionHistory;

        public BankAccount()
        {
            GenerateAccountNumber();
            transactionHistory = new Queue<BankTransaction>();
        }

        public BankAccount(decimal initialBalance)
        {
            GenerateAccountNumber();
            balance = initialBalance;
            transactionHistory = new Queue<BankTransaction>();
        }

        public BankAccount(BankAccountType type)
        {
            GenerateAccountNumber();
            accountType = type;
            transactionHistory = new Queue<BankTransaction>();
        }

        public BankAccount(decimal initialBalance, BankAccountType type)
        {
            GenerateAccountNumber();
            balance = initialBalance;
            accountType = type;
            transactionHistory = new Queue<BankTransaction>();
        }

        private void GenerateAccountNumber()
        {
            accountNumber = nextAccountNumber;
            nextAccountNumber++;
        }

        public int GetAccountNumber()
        {
            return accountNumber;
        }

        public decimal GetBalance()
        {
            return balance;
        }

        public BankAccountType GetAccountType()
        {
            return accountType;
        }

        public void PrintAccountInfo()
        {
            Console.WriteLine("Номер счета: " + accountNumber);
            Console.WriteLine("Баланс: " + balance);
            Console.WriteLine("Тип счета: " + accountType);
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= balance)
            {
                balance -= amount;
                BankTransaction transaction = new BankTransaction(-amount);
                transactionHistory.Enqueue(transaction);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Deposit(decimal amount)
        {
            balance += amount;
            BankTransaction transaction = new BankTransaction(amount);
            transactionHistory.Enqueue(transaction);
        }

        public static void Transfer(BankAccount fromAccount, BankAccount toAccount, decimal amount)
        {
            bool canWithdraw = fromAccount.Withdraw(amount);

            if (canWithdraw)
            {
                toAccount.Deposit(amount);
                Console.WriteLine("\nУспешно переведено " + amount + " рублей со счета "
                    + fromAccount.GetAccountNumber() + " на счет " + toAccount.GetAccountNumber() + ".");
            }
            else
            {
                Console.WriteLine("\nОшибка при переводе. Недостаточно средств на счете " + fromAccount.GetAccountNumber() + ".");
            }
        }
        public void PrintTransactionHistory()
        {
            Console.WriteLine("История операций для счета " + accountNumber + ":");

            foreach (BankTransaction transaction in transactionHistory)
            {
                string transactionType = transaction.TransactionAmount >= 0 ? "Пополнение" : "Снятие";
                decimal transactionAmount = Math.Abs(transaction.TransactionAmount);

                Console.WriteLine(transaction.TransactionDateTime + " - " + transactionType + " на сумму " + transactionAmount);
            }
        }
    }
}
