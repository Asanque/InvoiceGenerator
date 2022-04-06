using ClosedXML.Excel;
using System.Globalization;

namespace HaviSzamla
{
    public class Startup
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
        private ShopData shopData;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Program/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Szamlazo}/{action=Index}/{id?}");
            });

            SetupInMemoryDatabases();
        }

        private void SetupInMemoryDatabases()
        {
            
            var sheetLocation = GetLocation();
            var workbook = GetWorkbook(sheetLocation);
            var sheet = GetSheet(workbook, "haviszla");

            int monthNum = Convert.ToInt32(sheet.Row(2).Cell(10).Value);
            int weeksInMonth = Convert.ToInt32(sheet.Row(2).Cell(11).Value);
            string month = new DateTime(1, monthNum, 1).ToString("MMMM", new CultureInfo("hu-HU"));
            ShopData.SetInstance(month, weeksInMonth);
            shopData = ShopData.GetInstance();
            LoadShopData(GetSheet(workbook, "adatok"));
            LoadProductData(sheet);
        }

        private void LoadProductData(IXLWorksheet sheet)
        {
            var shopDao = ShopDao.GetInstance();
            int count = 0;
            foreach (var row in sheet.Rows())
            {
                if (count > 1)
                {
                    if (row.Cell(ShopNumberCol).Value.ToString() == "")
                    { break; }
                    int shopNum = Convert.ToInt32(row.Cell(ShopNumberCol).Value);
                    string productName = row.Cell(ProductCol).Value.ToString();
                    decimal amount = Convert.ToDecimal(row.Cell(AmountCol).Value);
                    int price = Convert.ToInt32(row.Cell(PriceCol).Value);
                    string unit = row.Cell(UnitCol).Value.ToString();
                    int weekNum = Convert.ToInt32(row.Cell(WeekNumCol).Value);
                    shopDao.AddItem(shopNum, productName, unit, price, weekNum, amount);
                }

                count++;
            }

            foreach (var shop in shopDao.data)
            {
                shop.AddTotalToItems();
                shop.SetTotals();
            }
        }

        private string GetLocation()
        {
            try 
            { 
                return File.ReadAllText(@"DocLocation.txt");
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
                shopData.AddToNameList(row.Cell(NameCol).Value.ToString());
                shopData.AddToAddressList(row.Cell(AddressCol).Value.ToString());
                shopData.AddToVatList(row.Cell(VatCol).Value.ToString());
            }
        }
    }
}
