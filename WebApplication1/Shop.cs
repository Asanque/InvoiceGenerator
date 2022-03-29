
namespace HaviSzamla
{
    public class Shop
    {
        public int ShopNumber { get; }
        public string Month { get; }
        public int WeeksInMonth { get; }
        public Dictionary<string, Dictionary<string, string>> DictOfItems { get; }
        public List<int> TotalPerWeek { get; }
        public int TotalInMonth { get; private set; }
        public string ShopName { get; }
        public string ShopVatNumber { get; }
        public string ShopAddress { get; }

        public Shop(string month, string shopName, string shopAddress, string shopVatNumber, int weeksInMonth, int shopNumber)
        {
            ShopNumber = shopNumber;
            ShopName = shopName;
            ShopVatNumber = shopVatNumber;
            ShopAddress = shopAddress;
            WeeksInMonth = weeksInMonth;
            DictOfItems = new Dictionary<string, Dictionary<string, string>>();
            Month = month;
            TotalPerWeek = new List<int>();
        }

        public void AddItemToDict(string itemName, string unit, string price)
        {
            var newDict = new Dictionary<string, string>();
            newDict.Add("unit", unit);
            newDict.Add("price", price);
            for (int i = 1; i <= WeeksInMonth; i++)
            {
                newDict.Add($"week{i}", "0");
            }
            newDict.Add("total", "0");
            DictOfItems.Add(itemName, newDict);
        }

        public void AddValueToItem(string itemName, string key, decimal amount)
        {
            DictOfItems[itemName][key] = (decimal.Parse(DictOfItems[itemName][key]) + amount).ToString();
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
                decimal total = 0;
                for (int i = 1; i <= WeeksInMonth; i++)
                {
                    total += decimal.Parse(DictOfItems[itemName][$"week{i}"]);
                }
                AddValueToItem(itemName, "total", total);
            }
        }

        public void SetTotals()
        {
            for (int i = 1; i <= WeeksInMonth; i++)
            {
                int currentTotal = 0;
                foreach (string itemName in DictOfItems.Keys)
                {
                    currentTotal += (int)(decimal.Parse(DictOfItems[itemName][$"week{i}"]) * int.Parse(DictOfItems[itemName]["price"]));
                }
                TotalPerWeek.Add(currentTotal);
            }
            TotalInMonth = TotalPerWeek.Sum();
        }
    }
}
