using Microsoft.Extensions.Logging;
using System;

namespace BankingUserPortal.Models
{
    public class InvestmentAccount : Account
    {
        public string InvestmentType { get; set; }
        public decimal InvestedAmount { get; private set; }

        private readonly ILogger<InvestmentAccount> _logger;

        public InvestmentAccount(ILogger<InvestmentAccount> logger) : base(logger)
        {
            _logger = logger;
            InvestedAmount = 0;
        }
        public override void Deposit(decimal amount)
        {
            Balance += amount;
            _logger.LogInformation("Deposited {Amount:C} to Investment Account ID: {AccountId}. New Balance: {Balance:C}", amount, AccountId, Balance);
        }

        public override void Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                _logger.LogInformation("Withdrew {Amount:C} from Investment Account ID: {AccountId}. New Balance: {Balance:C}", amount, AccountId, Balance);
            }
            else
            {
                _logger.LogWarning("Insufficient funds for withdrawal in Investment Account ID: {AccountId}. Attempted Amount: {Amount:C}", AccountId, amount);
                throw new InvalidOperationException("Insufficient funds");
            }
        }

        public override void Invest(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                InvestedAmount += amount;
                _logger.LogInformation("Invested {Amount:C} in Investment Account ID: {AccountId}. Total Invested Amount: {InvestedAmount:C}", amount, AccountId, InvestedAmount);
                // Additional investment logic here
            }
            else
            {
                _logger.LogWarning("Insufficient funds for investment in Investment Account ID: {AccountId}. Attempted Amount: {Amount:C}", AccountId, amount);
                throw new InvalidOperationException("Insufficient funds for investment");
            }
        }

        public void Invest(decimal amount, string investmentType)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                InvestedAmount += amount;
                InvestmentType = investmentType;
                _logger.LogInformation("Invested {Amount:C} with type {InvestmentType} in Investment Account ID: {AccountId}. Total Invested Amount: {InvestedAmount:C}", amount, investmentType, AccountId, InvestedAmount);
                // Different investment logic here based on investmentType
            }
            else
            {
                _logger.LogWarning("Insufficient funds for investment with type {InvestmentType} in Investment Account ID: {AccountId}. Attempted Amount: {Amount:C}", investmentType, AccountId, amount);
                throw new InvalidOperationException("Insufficient funds for investment");
            }
        }

        public override string CheckBalance(bool includeCurrencySymbol)
        {
            string balance = base.CheckBalance(includeCurrencySymbol);
            string message = $"Investment Account Balance: {balance}, Invested Amount: {(includeCurrencySymbol ? "$" : "")}{InvestedAmount}";
            _logger.LogDebug("Checked balance for Investment Account ID: {AccountId}. {Message}", AccountId, message);
            return message;
        }

        public decimal CalculateReturns()
        {
            // Implement logic to calculate investment returns
            // This is a placeholder implementation
            decimal returns = InvestedAmount * 0.05m; // Assuming 5% return
            _logger.LogDebug("Calculated returns for Investment Account ID: {AccountId}. Returns: {Returns:C}", AccountId, returns);
            return returns;
        }
    }
}
