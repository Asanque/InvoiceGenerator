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
            int monthNum = 1;
            string month = new DateTime(1, monthNum, 1).ToString("MMMM");
            ShopData.SetInstance(month, 5);
            var shopDao = ShopDao.GetInstance();
            
            shopDao.AddItem(0, "Kremes", "ft/db", "300", "week1", 5);
            shopDao.AddItem(1, "Kremes", "ft/db", "300", "week1", 5);
            shopDao.AddItem(1, "asd", "ft/db", "300", "week1", 5);
            shopDao.AddItem(1, "asd", "ft/db", "300", "week3", 5);

            foreach (var shop in shopDao.data)
            {
                shop.AddTotalToItems();
            }

        }
    }
}
