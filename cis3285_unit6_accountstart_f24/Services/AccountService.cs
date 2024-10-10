using System;
using System.Collections.Generic;
using Domain;

namespace Services
{
    public class AccountService : IAccountServices
    {
        // Store the accounts in a dictionary indexed by the account name
        private Dictionary<string, AccountBase> accountsDictionary;

        public AccountService()
        {
            // Instantiate the dictionary for accounts
            accountsDictionary = new Dictionary<string, AccountBase>();
        }

        public List<string> GetAllAccounts()
        {
            // Get a list of all account names
            return new List<string>(accountsDictionary.Keys);
        }

        public void CreateAccount(string accountName, AccountType accountType)
        {
            // Check for duplicate account name
            if (accountsDictionary.ContainsKey(accountName))
            {
                throw new InvalidOperationException($"Account with name '{accountName}' already exists.");
            }

            // Create a new account if not a duplicate
            AccountBase newAccount = AccountBase.CreateAccount(accountType);
            accountsDictionary.Add(accountName, newAccount);
        }

        public decimal GetAccountBalance(string accountName)
        {
            // Find the balance of the given account
            AccountBase acc = FindAccount(accountName);
            if (acc == null)
            {
                throw new KeyNotFoundException($"Account '{accountName}' not found.");
            }
            return acc.Balance;
        }

        public int GetRewardPoints(string accountName)
        {
            // Find the reward points of the given account
            AccountBase acc = FindAccount(accountName);
            if (acc == null)
            {
                throw new KeyNotFoundException($"Account '{accountName}' not found.");
            }
            return acc.RewardPoints;
        }

        public void Deposit(string accountName, decimal amount)
        {
            // Deposit the given amount into the account
            AccountBase acc = FindAccount(accountName);
            if (acc == null)
            {
                throw new KeyNotFoundException($"Account '{accountName}' not found.");
            }

            acc.AddTransaction(amount);
        }

        public bool Withdrawal(string accountName, decimal amount)
        {
            // Find the account using the account name
            AccountBase acc = FindAccount(accountName);
            if (acc == null)
            {
                return false;  // Account does not exist
            }

            // Check if the account has sufficient funds for withdrawal
            if (acc.Balance >= amount)
            {
                acc.AddTransaction(-amount);  // Withdraw by adding a negative amount
                return true;  // Withdrawal successful
            }
            else
            {
                return false;  // Insufficient funds
            }
        }

        private AccountBase FindAccount(string accountName)
        {
            if (accountsDictionary.ContainsKey(accountName))
            {
                return accountsDictionary[accountName];
            }
            return null;  // Account not found
        }
    }
}
