namespace BankingUserPortal.Models.Abstracts
{
    public abstract class BaseAccount
{
    public virtual int AccountId { get; set; }
    public virtual decimal Balance { get; set; }
    public int UserId { get; set; }

    // Abstract methods for deposit and withdraw
    public abstract void Deposit(decimal amount);
    public abstract void Withdraw(decimal amount);

    // Virtual method to check balance with/without currency symbol
    public virtual string CheckBalance(bool includeCurrencySymbol)
    {
        return includeCurrencySymbol ? $"${Balance}" : Balance.ToString();
    }

    // Overloaded method to check balance with/without precision
    public string CheckBalance(int precision)
    {
        return Balance.ToString($"F{precision}");
    }
}

}
