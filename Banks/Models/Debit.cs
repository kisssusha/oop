using System;
using Banks.Servises;
using Banks.Tools;

namespace Banks.Models
{
    public class Debit : IAccount
    {
        private double _benefit = 0;
        public Debit(int balance, double percent, int id)
        {
            Percent = percent;
            Balance = balance;
            Id = id;
        }

        public double Percent { get; }
        public int Balance { get; set; }
        public int Id { get; }

        public void WithdrawalOperation(int sum)
        {
            Balance -= sum;
        }

        public void CancelWithdrawalOperation(int sum)
        {
            Balance += sum;
        }

        public void RefillOperation(int sum)
        {
            Balance += sum;
        }

        public void CancelRefillOperation(int sum)
        {
            Balance -= sum;
        }

        public void TransferOperation(IAccount account2, int sum)
        {
            Balance -= sum;
            account2.RefillOperation(sum);
        }

        public void CancelTransferOperation(IAccount other, int sum)
        {
            Balance += sum;
            other.WithdrawalOperation(sum);
        }

        public void BenefitPay(int time)
        {
            _benefit = Balance * Percent / 300;
            Balance += (int)_benefit * time;
            _benefit = 0;
        }

        public bool Withdraw(int sum)
        {
            return Balance >= sum;
        }

        public int GetAccountId()
        {
            return Id;
        }

        public int CheckBalance()
        {
            return Balance;
        }
    }
}