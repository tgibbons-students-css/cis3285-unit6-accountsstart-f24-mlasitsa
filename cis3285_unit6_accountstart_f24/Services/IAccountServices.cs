using System;
using System.Collections.Generic;
using Domain;

namespace Services
{
    public interface IAccountServices
    {
        List<string> GetAllAccounts();
        void CreateAccount(string accountName, AccountType accountType);
        decimal GetAccountBalance(string accountName);
        int GetRewardPoints(string accountName);
        void Deposit(string accountName, decimal amount);

        // Updated to return bool to reflect success/failure of the withdrawal
        bool Withdrawal(string accountName, decimal amount);
    }
}
