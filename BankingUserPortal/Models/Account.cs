using BankingUserPortal.Models.Abstracts;
using BankingUserPortal.Models.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingUserPortal.Models
{
    public class Account : BaseAccount, ITransaction
{
    private static List<Account> _accounts = new List<Account>();
    private static int _lastId = 0;
    private readonly ILogger<Account> _logger;

    public string AccountType { get; set; }

    public Account(ILogger<Account> logger)
    {
        AccountId = System.Threading.Interlocked.Increment(ref _lastId);
        _logger = logger;
        _logger.LogDebug("Account created with ID: {AccountId}", AccountId);
    }

    public virtual void Invest(decimal amount)
    {
        _logger.LogDebug("Invest method not implemented for Account ID: {AccountId}", AccountId);
        throw new NotImplementedException("Basic accounts do not support investments.");
    }

    public override void Deposit(decimal amount)
    {
        Balance += amount;
        _logger.LogInformation("Deposited {Amount:C} to Account ID: {AccountId}. New Balance: {Balance:C}", amount, AccountId, Balance);
    }

    public override void Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            _logger.LogInformation("Withdrew {Amount:C} from Account ID: {AccountId}. New Balance: {Balance:C}", amount, AccountId, Balance);
        }
        else
        {
            _logger.LogWarning("Insufficient funds for withdrawal in Account ID: {AccountId}. Attempted Amount: {Amount:C}", AccountId, amount);
            throw new InvalidOperationException("Insufficient funds");
        }
    }

    public void Deposit(decimal amount, decimal conversionRate)
    {
        Balance += amount * conversionRate;
        _logger.LogInformation("Deposited {Amount:C} with conversion rate {ConversionRate} to Account ID: {AccountId}. New Balance: {Balance:C}", amount, conversionRate, AccountId, Balance);
    }

    public override string CheckBalance(bool includeCurrencySymbol)
    {
        var balance = includeCurrencySymbol ? $"${Balance}" : Balance.ToString();
        _logger.LogDebug("Checked balance for Account ID: {AccountId}. Balance: {Balance:C}", AccountId, Balance);
        return balance;
    }

    public static Account CreateAccount(Account account)
    {
        _accounts.Add(account);
        // Logging when a new account is created
        var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Account>();
        logger.LogInformation("Created new account with ID: {AccountId}", account.AccountId);
        return account;
    }

    public static Account GetAccount(int id)
    {
        var account = _accounts.FirstOrDefault(a => a.AccountId == id);
        var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Account>();
        if (account != null)
        {
            logger.LogDebug("Retrieved account with ID: {AccountId}", id);
        }
        else
        {
            logger.LogWarning("Account with ID: {AccountId} not found", id);
        }
        return account;
    }
}

}
