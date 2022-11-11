using InvoiceGenerator.Api.Gateways;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Core.UseCases;
using Microsoft.AspNetCore.Mvc.Razor;

namespace InvoiceGenerator.Api
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
            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add
                    ("/InvoiceGenerator.Api/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add
                    ("/InvoiceGenerator.Api/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
            });
            SetupDependency(services);
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
            InMemoryDatabaseGenerator generator = app.ApplicationServices.GetRequiredService<InMemoryDatabaseGenerator>();
            generator.SetupInMemoryDatabases();
        }

        public void SetupDependency(IServiceCollection services)
        {
            services.AddSingleton<IShopData, ShopData>();
            services.AddSingleton<IShopRepository, ShopRepository>();
            services.AddSingleton<InMemoryDatabaseGenerator>();
        }

        
    }
}
