using System;
using Banks.Servises;
using Banks.Tools;

namespace Banks.Models
{
    public class Credit : IAccount
    {
        private double _commission = 0;
        public Credit(int id, double commission, double limit, int balance)
        {
            Commission = commission;
            Limit = limit;
            Id = id;
            Balance = balance;
        }

        public double Commission { get; }
        public int Balance { get; set; }
        public double Limit { get; }
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
            _commission += Balance * Commission / 300;
            Balance -= (int)_commission * time;
            _commission = 0;
        }

        public bool Withdraw(int sum)
        {
            return Math.Abs(Balance - sum) < Limit;
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