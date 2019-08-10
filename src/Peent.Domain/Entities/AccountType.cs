namespace Peent.Domain.Entities
{
    public enum AccountType : short
    {
        Unknown = 0,
        Asset = 1,
        Cash = 2,
        Expense = 3,
        Revenue = 4,
        Debt = 5,
        Loan = 6,
        Mortgage = 7,
        InitialBalance = 8,
        Reconciliation = 9
    }
}
