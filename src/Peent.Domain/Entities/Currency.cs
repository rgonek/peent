namespace Peent.Domain.Entities
{
    public class Currency
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public ushort DecimalPlaces { get; set; }
    }
}
