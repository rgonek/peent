namespace Peent.Domain.Entities
{
    public enum AccountType : short
    {
        Unknown = 0,
        Asset = 1,
        Expense = 2,
        Revenue = 3,
        Liabilities = 4,
        InitialBalance = 5,
        Reconciliation = 6
    }
}
