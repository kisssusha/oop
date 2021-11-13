using System;
using System.Diagnostics.CodeAnalysis;
using Banks.Servises;
using Banks.Tools;

namespace Banks.Models
{
    public class Deposit : IAccount
    {
        private double _benefit = 0;
        public Deposit(int id, int balance, int time, double percent)
        {
            Id = id;
            Time = time;
            Percent = percent;
            Balance = balance;
        }

        public int Time { get; }
        public int Id { get; }
        public double Percent { get; }
        public int Balance { get; set; }

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

        public void TransferOperation(IAccount other, int sum)
        {
            Balance -= sum;
            other.RefillOperation(sum);
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
            return Balance >= sum && Time == 0;
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