using ClosedXML.Excel;
using System.Globalization;

namespace HaviSzamla
{
    public class Startup
    {
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
            string Mysheet = @"C:\Users\kisge\Desktop\testExcel.xlsm";
            var workbook = new XLWorkbook(Mysheet);
            var szamla = workbook.Worksheet("haviszla");

            int monthNum = Convert.ToInt32(szamla.Row(2).Cell(10).Value);
            int weeksInMonth = Convert.ToInt32(szamla.Row(2).Cell(11).Value);
            string month = new DateTime(1, monthNum, 1).ToString("MMMM", new CultureInfo("hu-HU"));
            ShopData.SetInstance(month, weeksInMonth);
            var shopDao = ShopDao.GetInstance();
            int count = 0;
            foreach (var row in szamla.Rows())
            {
                if (count > 3)
                {
                    if (row.Cell(1).Value.ToString() == "")
                    { break; }
                    int shopNum = Convert.ToInt32(row.Cell(1).Value);
                    string itemName = row.Cell(2).Value.ToString();
                    string unit = row.Cell(5).Value.ToString();
                    string price = row.Cell(4).Value.ToString();
                    string weekNum = "week" + row.Cell(6).Value.ToString();
                    decimal amount = Convert.ToDecimal(row.Cell(3).Value);
                    shopDao.AddItem(shopNum, itemName, unit, price, weekNum, amount);
                }
                
                count++;
            }

            foreach (var shop in shopDao.data)
            {
                shop.AddTotalToItems();
                shop.SetTotals();
            }

        }
    }
}
