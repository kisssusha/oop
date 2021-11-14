using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Backups.Tools;
using Banks.Servises;
using Banks.Tools;

namespace Banks.Models
{
    public class CentralBank
    {
        private static int _countOfId = 0;
        private List<Bank> _banks;

        public CentralBank()
        {
            _banks = new List<Bank>();
        }

        public ReadOnlyCollection<Bank> Banks => _banks.AsReadOnly();

        public void RegisterBank(Bank bank)
        {
            if (bank == null) throw new BackupsException("Invalid Bank");
            _banks.Add(bank);
        }

        public IAccount CreationAccount(Bank bank, int balance, Client client, int time, string option)
        {
            if (option == null) throw new BackupsException("Invalid option");
            if (client == null) throw new BackupsException("Invalid client");
            if (bank == null) throw new BackupsException("Invalid Bank");
            if (_banks.Any(b => b == bank))
            {
                return CreationAccountForClient(bank, client, balance, time, option);
            }
            else
            {
                throw new BanksException("Invalid bank");
            }
        }

        public void CheckTime(int time, IAccount account)
        {
            if (account == null) throw new BackupsException("Invalid account");
            account.BenefitPay(time);
        }

        public IAccount CreationAccountForClient(Bank bank, Client client, int balance, int time, string option)
        {
            switch (option)
            {
                case "Credit":

                    var credit = new Credit(_countOfId++, bank.CommissionOfCredit, bank.LimitOfCredit, balance);
                    bank.AddCreditInIAccount(credit);
                    return credit;

                case "Debit":

                    var debit = new Debit(balance, bank.PercentOfDebit, _countOfId++);
                    bank.AddDebitInIAccount(debit);
                    return debit;

                case "Deposit":

                    double depositPercent = 0;
                    foreach (var percent in bank.PercentOfDeposit.Percents)
                    {
                        if (balance < percent.sum)
                        {
                            depositPercent = percent.percent;
                        }
                    }

                    if (depositPercent == 0)
                    {
                        depositPercent = bank.PercentOfDeposit.Percents[^1].percent;
                    }

                    var deposit = new Deposit(_countOfId++, balance, time, depositPercent);
                    bank.AddDepositInIAccount(deposit);
                    return deposit;
                default:
                    throw new BanksException("Option not found");
            }
        }

        public void RefillMoneyOn(Bank bank, int balance, int id)
        {
            if (bank == null) throw new BackupsException("Invalid Bank");
            bank.GetAccountId(id).RefillOperation(balance);
        }

        public void WithdrawalMoney(Bank bank, int balance, int id, Client client)
        {
            if (!CheckClientPassportAndAddress(client) && balance > bank.Limitation && bank.IsOperationInAccount(balance))
            {
                throw new BanksException("Invalid Withdrawal");
            }

            bank.GetAccountId(id).WithdrawalOperation(balance);
        }

        public void TransferMoneyOnBalance(Bank bank1, int balance, int id1, Bank bank2, int id2, Client client)
        {
            if (!CheckClientPassportAndAddress(client) && balance > bank1.Limitation && bank1.IsOperationInAccount(balance))
            {
                throw new BanksException("Invalid Transfer");
            }

            bank1.GetAccountId(id1).TransferOperation(bank2.GetAccountId(id2), balance);
        }

        public void CancelRefill(Bank bank, int balance, int id)
        {
            var operation = bank.GetAccountId(id);
            if (operation == null) throw new BanksException("Refill don't exists");
            operation.CancelRefillOperation(balance);
        }

        public void CancelTransfer(Bank bank1, int balance, int id1, Bank bank2, int id2)
        {
            var operation = bank1.GetAccountId(id1);
            if (operation == null) throw new BanksException("Transfer don't exists");
            operation.CancelTransferOperation(bank2.GetAccountId(id2), balance);
        }

        public void CancelWithdrawal(Bank bank, int balance, int id)
        {
            var operation = bank.GetAccountId(id);
            if (operation == null) throw new BanksException("Withdrawal don't exists");
            operation.CancelWithdrawalOperation(balance);
        }

        private bool CheckClientPassportAndAddress(Client client) =>
            !string.IsNullOrEmpty(client.Passport) || !string.IsNullOrEmpty(client.Address);
    }
}