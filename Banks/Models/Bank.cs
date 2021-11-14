using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using Backups.Tools;
using Banks.Servises;
using Banks.Tools;

namespace Banks.Models
{
    public class Bank
    {
        private readonly List<Client> _clients;
        private readonly List<IAccount> _accountsOfClient;

        public Bank(string name, double commissionOfCredit, double limitOfCredit, double percentOfDebit, double limitation, PercentOfDeposit percentOfDeposit)
        {
            PercentOfDeposit = percentOfDeposit ?? throw new BackupsException("Invalid percents of deposit");
            Name = name ?? throw new BackupsException("Invalid name");
            CommissionOfCredit = commissionOfCredit;
            LimitOfCredit = limitOfCredit;
            PercentOfDebit = percentOfDebit;
            _clients = new List<Client>();
            Limitation = limitation;
            _accountsOfClient = new List<IAccount>();
        }

        public delegate void ChangeParametersOfBanks(object sender,  double newParameter);
        public event ChangeParametersOfBanks ChangeParametersOfBank;

        public ReadOnlyCollection<Client> Clients => _clients.AsReadOnly();

        public ReadOnlyCollection<IAccount> AccountsOfClient => _accountsOfClient.AsReadOnly();

        public PercentOfDeposit PercentOfDeposit { get; }
        public string Name { get; }
        public double CommissionOfCredit { get; private set; }
        public double LimitOfCredit { get; private set; }
        public double PercentOfDebit { get; private set; }
        public double Limitation { get; private set; }

        public void SetCommissionOfCredit(double commissionOfCredit)
        {
            CommissionOfCredit = commissionOfCredit;
            ChangeParametersOfBank?.Invoke(this, commissionOfCredit);
        }

        public void SetLimitOfCredit(double limitOfCredit)
        {
            LimitOfCredit = limitOfCredit;
            ChangeParametersOfBank?.Invoke(this, limitOfCredit);
        }

        public void SetPercentOfDebit(double percentOfDebit)
        {
            PercentOfDebit = percentOfDebit;
            ChangeParametersOfBank?.Invoke(this, percentOfDebit);
        }

        public void SetLimitation(double limitation)
        {
            Limitation = limitation;
            ChangeParametersOfBank?.Invoke(this, limitation);
        }

        public void AddClientInBank(Client client)
        {
            if (client == null) throw new BackupsException("Invalid client");
            if (_clients.Any(n => n.Name == client.Name) && _clients.Any(n => n.Surname == client.Surname))
                throw new BanksException("Client already in use");
            _clients.Add(client);
        }

        public void AddDepositInIAccount(Deposit deposit)
        {
            if (deposit == null) throw new BackupsException("Invalid deposit");
            _accountsOfClient.Add(deposit);
        }

        public void AddDebitInIAccount(Debit debit)
        {
            if (debit == null) throw new BackupsException("Invalid debit");
            _accountsOfClient.Add(debit);
        }

        public void AddCreditInIAccount(Credit credit)
        {
            if (credit == null) throw new BackupsException("Invalid credit");
            _accountsOfClient.Add(credit);
        }

        public bool IsOperationInAccount(int sum)
        {
            return _accountsOfClient.All(acc => acc.Withdraw(sum));
        }

        public IAccount GetAccountId(int id)
        {
            return _accountsOfClient.FirstOrDefault(d => d.GetAccountId() == id);
        }
    }
}