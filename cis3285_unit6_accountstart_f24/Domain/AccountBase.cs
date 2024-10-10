using System;

namespace Domain
{
    public abstract class AccountBase
    {
        public static AccountBase CreateAccount(AccountType type)
        {
            AccountBase account = null;
            switch (type)
            {
                case AccountType.Silver:
                    account = new SilverAccount();
                    break;
                case AccountType.Gold:
                    account = new GoldAccount();
                    break;
                case AccountType.Platinum:
                    account = new PlatinumAccount();
                    break;
            }
            return account;
        }

        public decimal Balance
        {
            get;
            private set;
        }

        public int RewardPoints
        {
            get;
            private set;
        }

        // Modify the AddTransaction method
        public void AddTransaction(decimal amount)
        {
            // Only add reward points for positive transactions (deposits)
            if (amount > 0)
            {
                RewardPoints += CalculateRewardPoints(amount);
            }

            // Always update the balance regardless of the transaction being a deposit or withdrawal
            Balance += amount;
        }

        // Abstract method for calculating reward points
        public abstract int CalculateRewardPoints(decimal amount);
    }
}
