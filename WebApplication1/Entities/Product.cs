namespace HaviSzamla
{
    public class Product
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Price { get; set; }
        public List<decimal> AmountPerWeek { get; set; }
        public decimal TotalInMonth { get; set; }

        public Product(string name, string unit, int price, int weeksInMonth)
        {
            AmountPerWeek = new List<decimal>();
            for (int i = 0; i < weeksInMonth; i++)
            {
                AmountPerWeek.Add(0);
            }
            Name = name;
            Unit = unit;
            Price = price;
        }
    }
}
