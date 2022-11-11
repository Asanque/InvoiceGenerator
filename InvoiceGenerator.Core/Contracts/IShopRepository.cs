using System.Collections.Generic;
using InvoiceGenerator.Core.Entities;

namespace InvoiceGenerator.Core.Contracts
{
    public interface IShopRepository
    {
        public List<Shop> Data { get; }
        public Shop GetShop(int id);

        public List<int> GetShopIds();

        public void AddItem(int id, string? itemName, string? unit, int price, int weekNum, decimal amount);

        public Shop CreateShop(int id);
    }
}
