using System;
using System.Security.Principal;
using System.Collections.Generic;


namespace Tymakov_9
{
    enum BankAccountType
    {
        Now,
        Sber
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Упр 9.1");
            Console.WriteLine("Переписать класс BankAccount используя конструкторы");
            Console.WriteLine();

            BankAccount account1 = new BankAccount();
            account1.Deposit(1000.50m);

            BankAccount account2 = new BankAccount(BankAccountType.Sber);
            account2.Deposit(5000.75m);

            BankAccount account3 = new BankAccount(1500.50m, BankAccountType.Now);

            Console.WriteLine("Информация о счете 1:");
            account1.PrintAccountInfo();

            Console.WriteLine();

            Console.WriteLine("Информация о счете 2:");
            account2.PrintAccountInfo();

            Console.WriteLine();

            Console.WriteLine("Информация о счете 3:");
            account3.PrintAccountInfo();

            decimal transferAmount;
            Console.WriteLine();

            Console.WriteLine("Введите сумму, которую хотите перевести с одного счёта на другой");
            bool succes = decimal.TryParse(Console.ReadLine(), out transferAmount);
            if (succes)
            {
                BankAccount.Transfer(account1, account2, transferAmount);

                Console.WriteLine("\nИнформация о счете 1 после перевода:");
                account1.PrintAccountInfo();

                Console.WriteLine();

                Console.WriteLine("Информация о счете 2 после перевода:");
                account2.PrintAccountInfo();
            }
            else
            {
                Console.WriteLine("Неверно введено число");
            }
            Console.WriteLine();


            Console.WriteLine("Упр 9.2");
            Console.WriteLine("Создать новый класс BankTransaction");
            Console.WriteLine();

            account1.Deposit(1000m);
            account1.Withdraw(500m);
            account1.Deposit(200m);

            account1.PrintTransactionHistory();
            Console.WriteLine();
            Console.WriteLine("Баланс после всех операций: " + account1.GetBalance());
            Console.WriteLine();


            Console.WriteLine("Упр 9.3");
            Console.WriteLine("");

            Console.WriteLine();


            Console.WriteLine("ДЗ 9.1");
            Console.WriteLine("Cписок песен");
            Console.WriteLine();
            List<Song> songs = new List<Song>();

            // Создание и заполнение первой песни
            Song song1 = new Song("Lost in the echo", "Linkin Park");
            songs.Add(song1);

            // Создание и заполнение второй песни
            Song song2 = new Song("Skyfall", "Adele");
            songs.Add(song2);

            // Создание и заполнение третьей песни
            Song song3 = new Song("Bring Me To Life", "Evanescense");
            songs.Add(song3);

            // Вывод информации о каждой песне
            foreach (Song song in songs)
            {
                Console.WriteLine(song.Title());
            }

            Console.WriteLine();
            // Сравнение первой и второй песни
            if (song1.Equals(song2))
            {
                Console.WriteLine("Первая песня совпадает со второй песней");
            }
            else
            {
                Console.WriteLine("Первая песня не совпадает со второй песней");
            }
            Console.WriteLine();


            Console.ReadKey();
        }
    }
}
