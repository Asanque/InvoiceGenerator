using System;
using System.Globalization;
using System.IO;
using ClosedXML.Excel;
using InvoiceGenerator.Core.Contracts;

namespace InvoiceGenerator.Core.UseCases
{
    public class InMemoryDatabaseGenerator
    {
        private const int ShopNumberCol = 1;
        private const int ProductCol = 2;
        private const int AmountCol = 3;
        private const int PriceCol = 4;
        private const int UnitCol = 5;
        private const int WeekNumCol = 6;
        private const int NameCol = 1;
        private const int AddressCol = 2;
        private const int VatCol = 3;
        private IShopData _shopData;
        private IShopRepository _shopDao;

        public InMemoryDatabaseGenerator(IShopData shopData, IShopRepository shopDao)
        {
            _shopData = shopData;
            _shopDao = shopDao;
        }
        public void SetupInMemoryDatabases()
        {

            var sheetLocation = GetLocation();
            var workbook = GetWorkbook(sheetLocation);
            var sheet = GetSheet(workbook, "haviszla");

            int monthNum = Convert.ToInt32(sheet.Row(2).Cell(10).Value);
            int weeksInMonth = Convert.ToInt32(sheet.Row(2).Cell(11).Value);
            string month = new DateTime(1, monthNum, 1).ToString("MMMM", new CultureInfo("hu-HU"));
            _shopData.Month = month;
            _shopData.WeeksInMonth = weeksInMonth;

            LoadShopData(GetSheet(workbook, "adatok"));
            LoadProductData(sheet);
        }

        private void LoadProductData(IXLWorksheet sheet)
        {

            int count = 0;
            foreach (var row in sheet.Rows())
            {
                if (count > 2)
                {
                    if (row.Cell(ShopNumberCol).Value.ToString() == string.Empty)
                    { break; }
                    int shopNum = Convert.ToInt32(row.Cell(ShopNumberCol).Value);
                    string? productName = row.Cell(ProductCol).Value.ToString();
                    decimal amount = Convert.ToDecimal(row.Cell(AmountCol).Value);
                    int price = Convert.ToInt32(row.Cell(PriceCol).Value);
                    string? unit = row.Cell(UnitCol).Value.ToString();
                    int weekNum = Convert.ToInt32(row.Cell(WeekNumCol).Value);
                    _shopDao.AddItem(shopNum, productName, unit, price, weekNum, amount);
                }

                count++;
            }

            foreach (var shop in _shopDao.Data)
            {
                shop.AddTotalToItems();
                shop.SetTotals();
            }
        }

        private string GetLocation()
        {
            try
            {
                return File.ReadAllText(@"InvoiceGenerator.Core\Data\DocLocation.txt");
            }
            catch
            {
                throw new Exception("DocLocation.txt nem található.");
            }
        }

        private XLWorkbook GetWorkbook(string location)
        {
            try
            {
                return new XLWorkbook(location);

            }
            catch
            {
                throw new Exception("A megadott dokumentum nem található, vagy már meg van nyitva");
            }
        }

        private IXLWorksheet GetSheet(XLWorkbook workbook, string sheetName)
        {
            try
            {
                return workbook.Worksheet(sheetName);
            }
            catch
            {
                throw new Exception("A megadott lap nem található");
            }
        }

        private void LoadShopData(IXLWorksheet shopDataSheet)
        {
            foreach (var row in shopDataSheet.Rows())
            {
                if (row.Cell(NameCol).Value.ToString() == "")
                { break; }
                _shopData.AddToNameList(row.Cell(NameCol).Value.ToString());
                _shopData.AddToAddressList(row.Cell(AddressCol).Value.ToString());
                _shopData.AddToVatList(row.Cell(VatCol).Value.ToString());
            }
        }
    }
}
