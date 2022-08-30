
using System.Collections.Generic;
using DeepEqual.Syntax;
using HaviSzamla;
using NSubstitute;
using NUnit.Framework;


namespace SzamlazoTest
{
    [TestFixture]
    public class ShopDaoTests
    {
        private ShopData shopData;
        private ShopDao shopDao;

        [SetUp]
        public void Setup()
        {
            shopData = new ShopData();
            shopData.Month = "testMonth";
            shopData.WeeksInMonth = 1;
            shopData.AddToAddressList("testAddress");
            shopData.AddToNameList("testName");
            shopData.AddToVatList("testVat");
            
            shopDao = new ShopDao(shopData);
        }

        [Test]
        public void CreateShop()
        {
            shopDao.CreateShop(0);
            var newShop = new Shop("testMonth", "testName", "testAddress", "testVat", 1, 0);
            var testShop = shopDao.Data[0];
            testShop.ShouldDeepEqual(newShop);
        }

        [Test]
        public void GetShop()
        {
            shopDao.CreateShop(0);
            var testShop = shopDao.GetShop(0);
            var newShop = new Shop("testMonth", "testName", "testAddress", "testVat", 1, 0);
            testShop.ShouldDeepEqual(newShop);
        }

        [Test]
        public void GetShopIdWithOneShop()
        {
            shopDao.CreateShop(0);
            var actualList = shopDao.GetShopIds();
            var expectedList = new List<int>(){0};
            actualList.ShouldDeepEqual(expectedList);
        }

        [Test]
        public void GetShopIdWithMultipleShops()
        {
            shopData.AddToAddressList("testAddress1");
            shopData.AddToNameList("testName1");
            shopData.AddToVatList("testVat1");
            shopData.AddToAddressList("testAddress2");
            shopData.AddToNameList("testName2");
            shopData.AddToVatList("testVat2");
            shopDao.CreateShop(0);
            shopDao.CreateShop(1);
            shopDao.CreateShop(2);
            var actualList = shopDao.GetShopIds();
            var expectedList = new List<int>() { 0, 1, 2 };
            actualList.ShouldDeepEqual(expectedList);
        }

        [Test]
        public void GetShopToAdd()
        {
            shopDao.AddItem(0, "itemName", "unit", 100, 1, 10);
            var actualShop = shopDao.GetShop(0);
            var actualProduct = actualShop.ListOfItems[0];
            var expectedProduct = new Product("itemName", "unit", 100, 1);
            expectedProduct.AmountPerWeek[0] = 10;
            actualProduct.ShouldDeepEqual(expectedProduct);

        }
    }
}
