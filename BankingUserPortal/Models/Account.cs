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

        public new int AccountId { get; set; }
        public new decimal Balance { get; set; }
        public string AccountType { get; set; }

        public Account() { }

        public Account(string accountType, decimal initialBalance)
        {
            AccountId = System.Threading.Interlocked.Increment(ref _lastId);
            AccountType = accountType;
            Balance = initialBalance;
        }

        public virtual void Invest(decimal amount)
        {
            throw new NotImplementedException("Basic accounts do not support investments.");
        }

        public override void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public override void Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds");
            }
        }

        public void Deposit(decimal amount, decimal conversionRate)
        {
            Balance += amount * conversionRate;
        }

        public override string CheckBalance(bool includeCurrencySymbol)
        {
            return includeCurrencySymbol ? $"${Balance}" : Balance.ToString();
        }

        public static Account CreateAccount(Account account)
        {
            account.AccountId = System.Threading.Interlocked.Increment(ref _lastId);
            _accounts.Add(account);
            return account;
        }

        public static Account GetAccount(int id)
        {
            return _accounts.FirstOrDefault(a => a.AccountId == id);
        }
    }
}