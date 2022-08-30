using System;
using System.Collections.Generic;
using DeepEqual.Syntax;
using HaviSzamla;
using NUnit.Framework;


namespace SzamlazoTest
{
    [TestFixture]
    public class ShopTests
    {
        [Test]
        public void AddingToListShouldSaveProperly()
        {
            int weeksinMonth = 1;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            var product = new Product("itemName", "itemUnit", 100, weeksinMonth);

            shop.AddItemToList("itemName", "itemUnit", 100);

            product.ShouldDeepEqual(shop.ListOfItems[0]);
        }

        [Test]
        public void AddingNullAsNameThrowsException()
        {
            int weeksinMonth = 1;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            Assert.Throws<ArgumentNullException>(() => shop.AddItemToList(null, "itemUnit", 100));
        }

        [Test]
        public void AddingNullAsUnitThrowsException()
        {
            int weeksinMonth = 1;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);

            Assert.Throws<ArgumentNullException>(() => shop.AddItemToList("itemName", null, 100));
        }
        
        [Test]
        public void AddingAmountToProduct()
        {
            int weeksinMonth = 1;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            shop.AddItemToList("itemName", "itemUnit", 100);

            shop.AddValueToItem("itemName", 1, 10);
            var amountList = new List<decimal>() {10};

            amountList.ShouldDeepEqual(shop.ListOfItems[0].AmountPerWeek);
        }

        [Test]
        public void AddingAmountToProductWithMultipleWeeks()
        {
            int weeksinMonth = 2;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            shop.AddItemToList("itemName", "itemUnit", 100);

            shop.AddValueToItem("itemName", 1, 10);
            var amountList = new List<decimal>() { 10, 0 };

            amountList.ShouldDeepEqual(shop.ListOfItems[0].AmountPerWeek);
        }

        [Test]
        public void AddingAmountToProductWithMultipleWeeksAndAmounts()
        {
            int weeksinMonth = 2;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            shop.AddItemToList("itemName", "itemUnit", 100);

            shop.AddValueToItem("itemName", 1, 10);
            shop.AddValueToItem("itemName", 2, 10);
            var amountList = new List<decimal>() { 10, 10 };

            amountList.ShouldDeepEqual(shop.ListOfItems[0].AmountPerWeek);
        }

        [Test]
        public void AddingAmountToProductWithMultipleAmounts()
        {
            int weeksinMonth = 1;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            shop.AddItemToList("itemName", "itemUnit", 100);

            shop.AddValueToItem("itemName", 1, 10);
            shop.AddValueToItem("itemName", 1, 10);
            var amountList = new List<decimal>() { 20 };

            amountList.ShouldDeepEqual(shop.ListOfItems[0].AmountPerWeek);
        }

        [Test]
        public void AddingAmountContainingNullThrowsException()
        {
            int weeksinMonth = 1;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            shop.AddItemToList("itemName", "itemUnit", 100);

            Assert.Throws<ArgumentNullException>(() => shop.AddValueToItem(null, 1, 10));
        }

        [Test]
        public void CheckItemInList()
        {
            int weeksinMonth = 1;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            shop.AddItemToList("itemName", "itemUnit", 100);

            Assert.AreEqual(true, shop.CheckItemInList("itemName"));
        }

        [Test]
        public void CheckItemInListWithWrongName()
        {
            int weeksinMonth = 1;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            shop.AddItemToList("itemName", "itemUnit", 100);

            Assert.AreEqual(false, shop.CheckItemInList("wrongName"));
        }

        [Test]
        public void CheckItemInListWithNull()
        {
            int weeksinMonth = 1;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            shop.AddItemToList("itemName", "itemUnit", 100);

            Assert.AreEqual(false, shop.CheckItemInList(null));
        }

        [Test]
        public void AddTotalToItemsWithMultipleWeeksAndAmounts()
        {
            int weeksinMonth = 2;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            shop.AddItemToList("itemName", "itemUnit", 100);

            shop.AddValueToItem("itemName", 1, 10);
            shop.AddValueToItem("itemName", 2, 10);
            shop.AddTotalToItems();
            var total = 20;

            Assert.AreEqual(total, shop.ListOfItems[0].TotalInMonth);
        }

        [Test]
        public void SetTotalsWithOneProductMultipleAmountCheckTotalPerWeek()
        {
            int weeksinMonth = 2;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            shop.AddItemToList("itemName", "itemUnit", 100);

            shop.AddValueToItem("itemName", 1, 10);
            shop.AddValueToItem("itemName", 2, 10);
            shop.AddTotalToItems();
            shop.SetTotals();
            var TotalPerWeek = new List<int>() {1000, 1000 };

            TotalPerWeek.ShouldDeepEqual(shop.TotalPerWeek);
        }

        [Test]
        public void SetTotalsWithOneProductMultipleAmountCheckTotalInMonth()
        {
            int weeksinMonth = 2;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            shop.AddItemToList("itemName", "itemUnit", 100);

            shop.AddValueToItem("itemName", 1, 10);
            shop.AddValueToItem("itemName", 2, 10);
            shop.AddTotalToItems();
            shop.SetTotals();
            var Total = 2000;

            Assert.AreEqual(Total, shop.TotalInMonth);
        }

        [Test]
        public void SetTotalsWithMoreProductsMultipleAmountCheckTotalPerWeek()
        {
            int weeksinMonth = 2;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            shop.AddItemToList("itemName", "itemUnit", 100);
            shop.AddItemToList("itemName2", "itemUnit", 150);

            shop.AddValueToItem("itemName", 1, 10);
            shop.AddValueToItem("itemName", 2, 10);
            shop.AddValueToItem("itemName2", 1, 10);
            shop.AddValueToItem("itemName2", 2, 20);
            shop.AddTotalToItems();
            shop.SetTotals();
            var TotalPerWeek = new List<int>() { 2500, 4000 };

            TotalPerWeek.ShouldDeepEqual(shop.TotalPerWeek);
        }

        [Test]
        public void SetTotalsWithMoreProductsMultipleAmountCheckTotalInMonth()
        {
            int weeksinMonth = 2;
            var shop = new Shop("1", "testShop", "testAddress", "testVat", weeksinMonth, 1);
            shop.AddItemToList("itemName", "itemUnit", 100);
            shop.AddItemToList("itemName2", "itemUnit", 150);

            shop.AddValueToItem("itemName", 1, 10);
            shop.AddValueToItem("itemName", 2, 10);
            shop.AddValueToItem("itemName2", 1, 10);
            shop.AddValueToItem("itemName2", 2, 20);
            shop.AddTotalToItems();
            shop.SetTotals();
            var Total = 6500;

            Assert.AreEqual(Total, shop.TotalInMonth);
        }
    }
}