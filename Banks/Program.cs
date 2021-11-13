using System;
using System.Diagnostics;
using Backups.Services;
using Banks.Models;
using Banks.Servises;
using Banks.Tools;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var central = new CentralBank();
            Console.WriteLine("1 - Создание полного клиента ");
            Console.WriteLine("2 - Создание клиента с ограничениями");
            Console.WriteLine("Name, Surname, Address, Passport");
            string str = Console.ReadLine();
            var creation = new ClientBuilder();
            Client client = null;
            Bank bank = null;
            switch (str)
            {
                case "1":
                    client = creation.ChangeName(Console.ReadLine()).ChangeSurname(Console.ReadLine()).ChangeAddress(Console.ReadLine()).ChangePassport(Console.ReadLine()).Create();
                    break;
                case "2":
                    client = creation.ChangeName(Console.ReadLine()).ChangeSurname(Console.ReadLine()).Create();
                    break;
            }

            while (str != "exit")
            {
                Console.WriteLine("1 - Создание банка");
                Console.WriteLine("2 - Добавление клиента в банк");
                Console.WriteLine("3 - Создание счета в банке");
                Console.WriteLine("4 - Снятие денег со счета");
                Console.WriteLine("5 - Пополнение счета");
                Console.WriteLine("6 - Перевод денег");
                Console.WriteLine("7 - Отмена пополнения");
                Console.WriteLine("8 - Отмена снятия");
                Console.WriteLine("9 - Отмена перевода");
                Console.WriteLine("10 - Промотка времени");
                Console.WriteLine("exit - program finish");
                str = Console.ReadLine();
                switch (str)
                {
                    case "1":
                        var percent = new PercentOfDeposit();
                        for (int j = 0; j < 2; j++)
                        {
                            Console.WriteLine("Введите сумму и процент");
                            percent.AddPercentAndSum(Convert.ToInt32(Console.ReadLine()), Convert.ToDouble(Console.ReadLine()));
                        }

                        percent.SortPercents();
                        Console.WriteLine("Введите параметры банка");

                        bank = new Bank(
                            Console.ReadLine(),
                            Convert.ToDouble(Console.ReadLine()),
                            Convert.ToDouble(Console.ReadLine()),
                            Convert.ToDouble(Console.ReadLine()),
                            Convert.ToDouble(Console.ReadLine()),
                            percent);
                        central.RegisterBank(bank);
                        break;
                    case "2":
                        bank.AddClientInBank(client);
                        break;
                    case "3":
                        switch (Console.ReadLine())
                        {
                            case "1":
                                Console.WriteLine("Введите сумму и срок");
                                central.CreationAccount(bank, Convert.ToInt32(Console.ReadLine()), client, Convert.ToInt32(Console.ReadLine()), "Credit");
                                break;
                            case "2":
                                Console.WriteLine("Введите сумму и срок");
                                central.CreationAccount(bank, Convert.ToInt32(Console.ReadLine()), client, Convert.ToInt32(Console.ReadLine()), "Debit");
                                break;
                            case "3":
                                Console.WriteLine("Введите сумму и срок");
                                central.CreationAccount(bank, Convert.ToInt32(Console.ReadLine()), client, Convert.ToInt32(Console.ReadLine()), "Deposit");
                                break;
                            default:
                                throw new BanksException("Invalid option");
                        }

                        break;
                    case "4":
                        Console.WriteLine("Введите сумму и номер счета");
                        central.WithdrawalMoney(bank, Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()), client);
                        break;
                    case "5":
                        Console.WriteLine("Введите сумму и номер счета");
                        central.RefillMoneyOn(bank, Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
                        break;
                    case "6":
                        Console.WriteLine("Введите сумму, номер счета отправителя и номер счета получателя");
                        central.TransferMoneyOnBalance(bank, Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()), bank, Convert.ToInt32(Console.ReadLine()), client);
                        break;
                    case "7":
                        Console.WriteLine("Введите сумму и номер счета ");
                        central.CancelRefill(bank, Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
                        break;
                    case "8":
                        Console.WriteLine("Введите сумму и номер счета ");
                        central.CancelWithdrawal(bank, Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
                        break;
                    case "9":
                        Console.WriteLine("Введите сумму, номер счета отправителя и номер счета получателя");
                        central.CancelTransfer(bank, Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()), bank, Convert.ToInt32(Console.ReadLine()));
                        break;
                    case "10":
                        Console.WriteLine("Введите сумму и номер счета ");
                        central.CheckTime(Convert.ToInt32(Console.ReadLine()), bank.GetAccountId(Convert.ToInt32(Console.ReadLine())));
                        break;

                    case "exit":
                        Console.WriteLine("finish");
                        break;
                }
            }
        }
    }
}
