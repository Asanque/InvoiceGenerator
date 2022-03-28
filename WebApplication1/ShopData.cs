using System.Xml;

namespace HaviSzamla
{
    public class ShopData
    {
        public static List<string> ShopNameList { get; } = new () {"name1", "name2"};
        public static List<string> ShopAddressList { get; } = new () {"name1", "name2"};
        public static List<string> ShopVatsList { get; } = new () {"name1", "name2"};

        public string Month { get; }
        public int WeeksInMonth { get; }
        private static ShopData _instance = null;

        public ShopData(string month, int weeksInMonth)
        {
            Month = month;
            WeeksInMonth = weeksInMonth;
        }

        public static void SetInstance(string month, int weeksInMonth)
        {
            _instance = new ShopData(month, weeksInMonth);
        }

        public static ShopData GetInstance()
        {
            return _instance;
        }
    }
}
