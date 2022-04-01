using ClosedXML.Excel;
using System.Globalization;

namespace HaviSzamla
{
    public class Startup
    {
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
            string sheetLocation = string.Empty;
            try { sheetLocation = File.ReadAllText(@"DocLocation.txt"); }
            catch {
                Console.WriteLine("DocLocation.txt nem található.");
                Console.ReadLine();
            }
            XLWorkbook workbook = default;
            try
            {
                workbook = new XLWorkbook(sheetLocation);

            }
            catch
            {   
                Console.WriteLine("A megadott dokumentum nem található");
                Console.ReadLine();
            }
            var sheet = workbook.Worksheet("haviszla");

            int monthNum = Convert.ToInt32(sheet.Row(2).Cell(10).Value);
            int weeksInMonth = Convert.ToInt32(sheet.Row(2).Cell(11).Value);
            string month = new DateTime(1, monthNum, 1).ToString("MMMM", new CultureInfo("hu-HU"));
            ShopData.SetInstance(month, weeksInMonth);
            shopData = ShopData.GetInstance();
            LoadShopData(workbook.Worksheet("adatok"));
            var shopDao = ShopDao.GetInstance();
            int count = 0;
            foreach (var row in sheet.Rows())
            {
                if (count > 1)
                {
                    if (row.Cell(1).Value.ToString() == "")
                    { break; }
                    int shopNum = Convert.ToInt32(row.Cell(1).Value);
                    string itemName = row.Cell(2).Value.ToString();
                    decimal amount = Convert.ToDecimal(row.Cell(3).Value);
                    int price = Convert.ToInt32(row.Cell(4).Value);
                    string unit = row.Cell(5).Value.ToString();
                    int weekNum = Convert.ToInt32(row.Cell(6).Value);
                    shopDao.AddItem(shopNum, itemName, unit, price, weekNum, amount);
                }
                
                count++;
            }

            foreach (var shop in shopDao.data)
            {
                shop.AddTotalToItems();
                shop.SetTotals();
            }
            Console.WriteLine("You can now open the webpage");
        }

        private void LoadShopData(IXLWorksheet shopDataSheet)
        {
            foreach (var row in shopDataSheet.Rows())
            {
                if (row.Cell(1).Value.ToString() == "")
                { break; }
                shopData.AddToNameList(row.Cell(1).Value.ToString());
                shopData.AddToAddressList(row.Cell(2).Value.ToString());
                shopData.AddToVatList(row.Cell(3).Value.ToString());
            }
        }
    }
}
