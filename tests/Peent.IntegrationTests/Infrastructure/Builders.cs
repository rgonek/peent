namespace Peent.IntegrationTests.Infrastructure
{
    public class A
    {
        public static TransactionBuilder Transaction => new TransactionBuilder().WithRandomData();
        public static CategoryBuilder Category => new CategoryBuilder().WithRandomData();
        public static TagBuilder Tag => new TagBuilder().WithRandomData();
    }

    public class An
    {
        public static AccountBuilder Account => new AccountBuilder().WithRandomData();
    }
}
