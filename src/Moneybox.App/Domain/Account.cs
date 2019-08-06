using System;

namespace Moneybox.App
{
    public class Account
    {
        private const decimal PayInLimit = 4000m;

        private const decimal PayInLimitNotificationThreshold = 500m;

        private const decimal LowFundNotificationThreshold = 500m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

        public bool HasLowFunds => Balance < LowFundNotificationThreshold; // true when funds are below threshold (500)

        public bool IsNearPayLimit => PaidIn < PayInLimitNotificationThreshold; // true when PaidIn is within threshold amount (500) of PayLimit

        /// <summary>
        /// Takes amount from account if it's within Balance.
        /// </summary>
        /// <param name="amount">Decimal amount to withdraw</param>
        public void WithdrawMoney(decimal amount)
        {
            var newBalanceAmount = Balance - amount;
            if (newBalanceAmount < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make withdrawal");
            }

            Balance = newBalanceAmount; // store new account balance
            Withdrawn = Withdrawn - amount; // store new withdraw amount
        }

        /// <summary>
        /// Puts amount in account if it's within pay limit.
        /// </summary>
        /// <param name="amount">Decimal amount to withdraw</param>
        public void DepositMoney(decimal amount)
        {
            var PayInAmount = PaidIn + amount;
            if (PayInAmount > PayInLimit)
            {
                throw new InvalidCastException("Account pay in limit reached");
            }

            Balance = Balance + amount; // store new account balance
            PaidIn = PayInAmount; // store new PaidIn amount
        }
    }
}
