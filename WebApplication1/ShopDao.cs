

namespace HaviSzamla
{
    public class ShopDao
    {
        public List<Shop> data = new List<Shop>();
        private static ShopDao instance = null;
        private ShopData shopData;

        private ShopDao()
        {
            shopData = ShopData.GetInstance();
        }

        public static ShopDao GetInstance()
        {
            if (instance == null)
            {
                instance = new ShopDao();
            }
            return instance;
        }

        public Shop GetShop(int id)
        {
            return data.First(shop => shop.ShopNumber == id);
        }

        public List<int> GetShopIds()
        {
            return data.Select(shop => shop.ShopNumber).ToList();
        }

        public void AddItem(int id, string itemName, string unit, int price, int key, decimal amount)
        {

            var currentShop = GetShopToAdd(id);
            if (!currentShop.CheckItemInList(itemName))
            {
                currentShop.AddItemToList(itemName, unit, price);
            }
            currentShop.AddValueToItem(itemName, key, amount);
        }

        private Shop GetShopToAdd(int id)
        {
            try
            {
                return GetShop(id);
            }
            catch
            {
                var name = ShopData.ShopNameList[id];
                var address = ShopData.ShopAddressList[id];
                var vat = ShopData.ShopVatList[id];
                var newShop = new Shop(shopData.Month, name, address, vat, shopData.WeeksInMonth, id);
                data.Add(newShop);
                return newShop;
            }
        }
    }
}
