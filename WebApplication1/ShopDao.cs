

namespace HaviSzamla
{
    public class ShopDao : IShopDao
    {
        public List<Shop> Data { get; } = new List<Shop>();
        private IShopData shopData;

        public ShopDao(IShopData shopData)
        {
            this.shopData = shopData;
        }
        

        public Shop GetShop(int id)
        {
            return Data.First(shop => shop.ShopNumber == id);
        }

        public List<int> GetShopIds()
        {
            return Data.Select(shop => shop.ShopNumber).ToList();
        }

        public void AddItem(int id, string itemName, string unit, int price, int weekNum, decimal amount)
        {

            var currentShop = GetShopToAdd(id);
            if (!currentShop.CheckItemInList(itemName))
            {
                currentShop.AddItemToList(itemName, unit, price);
            }
            currentShop.AddValueToItem(itemName, weekNum, amount);
        }

        private Shop GetShopToAdd(int id)
        {
            try
            {
                return GetShop(id);
            }
            catch
            {
                return CreateShop(id);
            }
        }

        public Shop CreateShop(int id)
        {
            var name = ShopData.ShopNameList[id];
            var address = ShopData.ShopAddressList[id];
            var vat = ShopData.ShopVatList[id];
            var newShop = new Shop(shopData.Month, name, address, vat, shopData.WeeksInMonth, id);
            Data.Add(newShop);
            return newShop;
        }
    }
}
