
namespace HaviSzamla
{
    public class Shop
    {
        public int ShopNumber { get; }
        private readonly string _month;
        public int WeeksInMonth { get; }
        public Dictionary<string, Dictionary<string, string>> DictOfItems { get; }
        private readonly string _shopName;
        private readonly string _shopVatNumber;
        private readonly string _shopAddress;

        public Shop(string month, string shopName, string shopVatNumber, string shopAddress, int weeksInMonth, int shopNumber)
        {
            ShopNumber = shopNumber;
            _shopName = shopName;
            _shopVatNumber = shopVatNumber;
            _shopAddress = shopAddress;
            WeeksInMonth = weeksInMonth;
            DictOfItems = new Dictionary<string, Dictionary<string, string>>();
            _month = month;
        }

        public void AddItemToDict(string itemName, string unit, string price)
        {
            var newDict = new Dictionary<string, string>();
            newDict.Add("unit", unit);
            newDict.Add("price", price);
            newDict.Add("total", "0");
            for (int i = 1; i <= WeeksInMonth; i++)
            {
                newDict.Add($"week{i}", "0");
            }
            DictOfItems.Add(itemName, newDict);
        }

        public void AddValueToItem(string itemName, string key, int amount)
        {
            DictOfItems[itemName][key] = (int.Parse(DictOfItems[itemName][key]) + amount).ToString();
        }

        public bool CheckItemInList(string itemName)
        {
            if (DictOfItems.ContainsKey(itemName))
            {
                return true;
            }
            return false;
        }

        public void AddTotalToItems()
        {
            foreach (string itemName in DictOfItems.Keys)
            {
                int total = 0;
                for (int i = 1; i <= WeeksInMonth; i++)
                {
                    total += int.Parse(DictOfItems[itemName][$"week{i}"]);
                }
                AddValueToItem(itemName, "total", total);
            }
        }

        public string GetShopData()
        {
            return $"{_shopName}\n{_shopAddress}\n{_shopVatNumber}\n{_month}";
        }

        public string GetItemsData()
        {
            string data = string.Empty;
            int count = 0;
            foreach (string itemName in DictOfItems.Keys)
            {
                if (count != 0)
                {
                    data += "\n";
                }
                data += itemName;
                data += DictOfItems[itemName]["unit"];
                data += DictOfItems[itemName]["price"];
                for (int i = 1; i <= WeeksInMonth; i++)
                {
                    data += DictOfItems[itemName][$"week{i}"];
                }
                data += DictOfItems[itemName]["total"];
                count++;
            }
            return data;
        }
    }
}
