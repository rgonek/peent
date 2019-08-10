namespace Peent.Domain.Entities
{
    public enum TransactionType :short
    {
        Unknown = 0,
        Withdrawal = 1,
        Deposit = 2,
        Transfer = 3,
        OpeningBalance = 4,
        Reconciliation = 5
    }
}
