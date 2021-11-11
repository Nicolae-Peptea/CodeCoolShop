using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Stripe;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Codecool.CodecoolShop
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
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.AddTransient<IMailService, MailService>();

            //services.AddDbContext<CodeCoolShopContext>();
            string connectionString = Configuration.GetConnectionString("CodeCoolShop");
            services.AddDbContext<CodeCoolShopContext>(options =>
             options.UseSqlServer(connectionString)
         );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Product/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });

            SetupInMemoryDatabases();
        }

        private void SetupInMemoryDatabases()
        {
            IProductDao productDataStore = ProductDaoMemory.GetInstance();
            IProductCategoryDao productCategoryDataStore = ProductCategoryDaoMemory.GetInstance();
            ISupplierDao supplierDataStore = SupplierDaoMemory.GetInstance();

            Supplier amazon = new Supplier { Name = "Amazon", Description = "Digital content and services" };
            supplierDataStore.Add(amazon);
            Supplier lenovo = new Supplier { Name = "Lenovo", Description = "Computers" };
            supplierDataStore.Add(lenovo);
            Supplier apple = new Supplier { Name = "Apple", Description = "Consumer electronics, computer software, and online services." };
            supplierDataStore.Add(apple);

            ProductCategory tablet = new ProductCategory { Name = "Tablet", Department = "Hardware", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            productCategoryDataStore.Add(tablet);
            ProductCategory phone = new ProductCategory { Name = "Phone", Department = "Hardware", Description = "A mobile phone, cellular phone, cell phone, cellphone, handphone, or hand phone, sometimes shortened to simply mobile, cell or just phone." };
            productCategoryDataStore.Add(phone);

            productDataStore.Add(new Models.ShopProduct { Name = "Amazon Fire", DefaultPrice = 49.9m, Currency = "USD", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = tablet, Supplier = amazon });
            productDataStore.Add(new Models.ShopProduct { Name = "Lenovo IdeaPad Miix 700", DefaultPrice = 479.0m, Currency = "USD", Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", ProductCategory = tablet, Supplier = lenovo });
            productDataStore.Add(new Models.ShopProduct { Name = "Amazon Fire HD 8", DefaultPrice = 89.0m, Currency = "USD", Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", ProductCategory = tablet, Supplier = amazon });
            productDataStore.Add(new Models.ShopProduct { Name = "Apple iPhone 12 Pro Max", DefaultPrice = 1239.0m, Currency = "USD", Description = "The iPhone 12 is a new iPhone model developed by Apple Inc. It is part of a device family that was announced during a special event on October 13, 2020 to succeed the iPhone 11 line.", ProductCategory = phone, Supplier = apple });
        }
    }
}
