using Banks.Models;
using Banks.Servises;
using NUnit.Framework;

namespace BanksTest
{
    public class Tests
    {
        [Test]
        public void RegistrationBankTest()
        {
            var central = new CentralBank();
            var percent = new PercentOfDeposit();
            percent.AddPercentAndSum(12,12);
            percent.AddPercentAndSum(13,13);
            percent.SortPercents();

            var bank1 = new Bank("Alpha", 12, 23, 34,
                45, percent);
            var bank2 = new Bank("Sber", 12, 66, 34 ,45, percent);

            central.RegisterBank(bank1);
            central.RegisterBank(bank2);
            Assert.AreEqual(central.Banks.Count,  2);

        }
        [Test]
        public void AddClientInBankTest()
        {
            var creation1 = new ClientBuilder();
            Client client1 = creation1.ChangeName("Ksusha").ChangeSurname("Kolpikova").ChangeAddress("Vazemski").ChangePassport("266767").Create();

            var creation2 = new ClientBuilder();
            var persent = new PercentOfDeposit();
            persent.AddPercentAndSum(12,12);
            persent.AddPercentAndSum(13,13);
            persent.SortPercents();

            Client client2 = creation2.ChangeName("Bogdan").ChangeSurname("Kolpikov").ChangeAddress("Vazemski").ChangePassport("2667898").Create();
            var bank1 = new Bank("Alpha", 12, 23, 34,
                45, persent);
            bank1.AddClientInBank(client1);
            bank1.AddClientInBank(client2);
            Assert.True(bank1.Clients.Count == 2);
        }
        
        [Test]
        public void DebitAccountTest()
        {
            var creation1 = new ClientBuilder();
            Client client1 = creation1.ChangeName("Ksusha").ChangeSurname("Kolpikova").ChangeAddress("Vazemski").ChangePassport("266767").Create();

            var central = new CentralBank();
            var persent = new PercentOfDeposit();
            persent.AddPercentAndSum(12,12);
            persent.AddPercentAndSum(13,13);
            persent.SortPercents();

            var bank1 = new Bank("Alpha", 12, 23, 34,
                45, persent);
            
            bank1.AddClientInBank(client1);
            central.RegisterBank(bank1);
            central.CreationAccount(bank1, 499,client1,3, "Debit");
            Assert.True(bank1.AccountsOfClient.Count == 1);
        }
        [Test]
        public void DepositAccountTest()
        {
            var creation1 = new ClientBuilder();
            Client client1 = creation1.ChangeName("Ksusha").ChangeSurname("Kolpikova").ChangeAddress("Vazemski").ChangePassport("266767").Create();

            var central = new CentralBank();
            var persent = new PercentOfDeposit();
            persent.AddPercentAndSum(12,12);
            persent.AddPercentAndSum(13,13);
            persent.SortPercents();

            var bank1 = new Bank("Alpha", 12, 23, 34,
                45, persent);
            
            bank1.AddClientInBank(client1);
            central.RegisterBank(bank1);
            central.CreationAccount(bank1, 499,client1,3, "Deposit");
            
            Assert.True(bank1.AccountsOfClient.Count == 1);
        }
        [Test]
        public void CreditAccountTest()
        {
            var creation1 = new ClientBuilder();
            Client client1 = creation1.ChangeName("Ksusha").ChangeSurname("Kolpikova").ChangeAddress("Vazemski").ChangePassport("266767").Create();

            var central = new CentralBank();
            var persent = new PercentOfDeposit();
            persent.AddPercentAndSum(12,12);
            persent.AddPercentAndSum(13,13);
            persent.SortPercents();

            var bank1 = new Bank("Alpha", 12, 23, 34,
                45, persent);
            
            bank1.AddClientInBank(client1);
            central.RegisterBank(bank1);
            central.CreationAccount(bank1, 499,client1,3, "Credit");
            Assert.True(bank1.AccountsOfClient.Count == 1);
        }

        [Test]
        public void CheckTimeTest()
        {
            var creation1 = new ClientBuilder();
            Client client1 = creation1.ChangeName("Ksusha").ChangeSurname("Kolpikova").ChangeAddress("Vazemski").ChangePassport("266767").Create();

            var central = new CentralBank();
            var percent = new PercentOfDeposit();
            percent.AddPercentAndSum(12,12);
            percent.AddPercentAndSum(13,13);
            percent.SortPercents();

            var bank1 = new Bank("Alpha", 12, 23, 34,
                45, percent);
            
            bank1.AddClientInBank(client1);
            central.RegisterBank(bank1);
            central.CreationAccount(bank1, 500,client1,3, "Debit");
            central.CreationAccount(bank1, 500,client1,3, "Deposit");
            central.CreationAccount(bank1, 500,client1,3, "Credit");
            
            central.CheckTime(4, bank1.AccountsOfClient[0]);
            central.CheckTime(4, bank1.AccountsOfClient[1]);
            central.CheckTime(4, bank1.AccountsOfClient[2]);
            
            Assert.True(bank1.AccountsOfClient[0].CheckBalance() == 724);
            Assert.True(bank1.AccountsOfClient[1].CheckBalance() == 584);
            Assert.True(bank1.AccountsOfClient[2].CheckBalance() == 420);
        }
    }
}