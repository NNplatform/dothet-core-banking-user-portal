namespace BankingUserPortal.Models.Interfaces
{
    public interface ITransaction
    {
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        void Invest(decimal amount);
    }
}
