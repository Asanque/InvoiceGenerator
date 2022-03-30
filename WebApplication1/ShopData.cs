using System.Xml;

namespace HaviSzamla
{
    public class ShopData
    {
        public static List<string> ShopNameList { get; } = new () {"name1", "name2", "name3", "name4", "name5", "name6", "name7", "name8", "name9", "name10", "name11", "name12", "name13", "name14", "name15", "name16", "name17", "name18", "name19", "name20", "name21", "name22" };
        public static List<string> ShopAddressList { get; } = new () { "address1", "address2", "address3", "address4", "address5", "address6", "address7", "address8", "address9", "address10", "address11", "address12", "address13", "address14", "address15", "address16", "address17", "address18", "address19", "address20", "address21", "address22" };
        public static List<string> ShopVatsList { get; } = new () { "VAT1", "VAT2", "VAT3", "VAT4", "VAT5", "VAT6", "VAT7", "VAT8", "VAT9", "VAT10", "VAT11", "VAT12", "VAT13", "VAT14", "VAT15", "VAT16", "VAT17", "VAT18", "VAT19", "VAT20", "VAT21", "VAT22" };

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
