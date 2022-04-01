using System.Xml;

namespace HaviSzamla
{
    public class ShopData
    {
        public static List<string> ShopNameList { get; } = new();
        public static List<string> ShopAddressList { get; } = new ();
        public static List<string> ShopVatList { get; } = new ();

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

        public void AddToNameList(string name)
        {
            ShopNameList.Add(name);
        }
        public void AddToAddressList(string address)
        {
            ShopAddressList.Add(address);
        }
        public void AddToVatList(string vat)
        {
            ShopVatList.Add(vat);
        }
    }
}
