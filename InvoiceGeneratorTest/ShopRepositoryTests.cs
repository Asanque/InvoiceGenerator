
using System.Collections.Generic;
using DeepEqual.Syntax;
using InvoiceGenerator.Api.Gateways;
using InvoiceGenerator.Core.Entities;
using NUnit.Framework;


namespace SzamlazoTest
{
    [TestFixture]
    public class shopRepositoryTests
    {
        private ShopData shopData;
        private ShopRepository shopRepository;

        [SetUp]
        public void Setup()
        {
            shopData = new ShopData();
            shopData.Month = "testMonth";
            shopData.WeeksInMonth = 1;
            shopData.AddToAddressList("testAddress");
            shopData.AddToNameList("testName");
            shopData.AddToVatList("testVat");
            
            shopRepository = new ShopRepository(shopData);
        }

        [Test]
        public void CreateShop()
        {
            shopRepository.CreateShop(0);
            var newShop = new Shop("testMonth", "testName", "testAddress", "testVat", 1, 0);
            var testShop = shopRepository.Data[0];
            testShop.ShouldDeepEqual(newShop);
        }

        [Test]
        public void GetShop()
        {
            shopRepository.CreateShop(0);
            var testShop = shopRepository.GetShop(0);
            var newShop = new Shop("testMonth", "testName", "testAddress", "testVat", 1, 0);
            testShop.ShouldDeepEqual(newShop);
        }

        [Test]
        public void GetShopIdWithOneShop()
        {
            shopRepository.CreateShop(0);
            var actualList = shopRepository.GetShopIds();
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
            shopRepository.CreateShop(0);
            shopRepository.CreateShop(1);
            shopRepository.CreateShop(2);
            var actualList = shopRepository.GetShopIds();
            var expectedList = new List<int>() { 0, 1, 2 };
            actualList.ShouldDeepEqual(expectedList);
        }

        [Test]
        public void GetShopToAdd()
        {
            shopRepository.AddItem(0, "itemName", "unit", 100, 1, 10);
            var actualShop = shopRepository.GetShop(0);
            var actualProduct = actualShop.ListOfItems[0];
            var expectedProduct = new Product("itemName", "unit", 100, 1);
            expectedProduct.AmountPerWeek[0] = 10;
            actualProduct.ShouldDeepEqual(expectedProduct);

        }
    }
}
