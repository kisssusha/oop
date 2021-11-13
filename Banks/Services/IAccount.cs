using Banks.Models;

namespace Banks.Servises
{
    public interface IAccount
    {
        public void WithdrawalOperation(int sum);
        public void CancelWithdrawalOperation(int sum);
        public void RefillOperation(int sum);
        public void CancelRefillOperation(int sum);
        public void TransferOperation(IAccount other, int sum);
        public void CancelTransferOperation(IAccount other, int sum);
        public void BenefitPay(int time);
        public bool Withdraw(int sum);
        public int GetAccountId();
        public int CheckBalance();
    }
}