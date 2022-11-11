using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceGenerator.Core.Entities
{
    public class Shop
    {
        public int ShopNumber { get; }
        public string Month { get; }
        public int WeeksInMonth { get; }
        public List<Product> ListOfItems { get; }
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
            ListOfItems = new List<Product>();
            Month = month;
            TotalPerWeek = new List<int>();
        }

        public void AddItemToList(string itemName, string unit, int price)
        {
            if (itemName == null || unit == null || price == null)
            {
                throw new ArgumentNullException();
            }
            ListOfItems.Add(new Product(itemName, unit, price, WeeksInMonth));
        }

        public void AddValueToItem(string itemName, int weekNum, decimal amount)
        {
            if (itemName == null || weekNum == null || amount == null)
            {
                throw new ArgumentNullException();
            }
            Product product = ListOfItems.First(product => product.Name == itemName);
            product.AmountPerWeek[weekNum - 1] += amount;
        }

        public bool CheckItemInList(string itemName)
        {
            return ListOfItems.Where(product => product.Name.Equals(itemName)).Any();
        }

        public void AddTotalToItems()
        {
            foreach (Product product in ListOfItems)
            {
                product.TotalInMonth = product.AmountPerWeek.Sum();
            }
        }

        public void SetTotals()
        {
            for (int i = 0; i < WeeksInMonth; i++)
            {
                int currentTotal = 0;
                foreach (Product product in ListOfItems)
                {
                    currentTotal += (int)(product.AmountPerWeek[i] * product.Price);
                }
                TotalPerWeek.Add(currentTotal);
            }
            TotalInMonth = TotalPerWeek.Sum();
        }
    }
}
