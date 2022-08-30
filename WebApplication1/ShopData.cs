using System.Xml;

namespace HaviSzamla
{
    public class ShopData : IShopData
    {
        public static List<string> ShopNameList { get; } = new();
        public static List<string> ShopAddressList { get; } = new ();
        public static List<string> ShopVatList { get; } = new ();

        public string? Month { get; set; }
        public int WeeksInMonth { get; set; }
        
        

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
