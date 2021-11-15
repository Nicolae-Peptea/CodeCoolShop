using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Stripe;

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
            services.Configure<SendgridSettings>(Configuration.GetSection("Sendgrid"));

            services.AddTransient<IProductDao, ProductDaoDb>();
            services.AddTransient<IProductCategoryDao, ProductCategoryDaoDb>();
            services.AddTransient<ISupplierDao, SupplierDaoDb>();
            services.AddTransient<IOrderDao, OrderDaoDb>();
            services.AddTransient<IProductOrder, ProductOrderDaoDb>();
            services.AddScoped<IProductServicesDb, ProductServicesDb>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IOrderServices, OrderServices>();

            services.AddScoped<IMailService, MailService>();

            string connectionString = Configuration.GetConnectionString("CodeCoolShop");
            services.AddDbContext<CodeCoolShopContext>(options =>
                options.UseSqlServer(connectionString)
            );

            services.AddIdentity<IdentityUser, IdentityRole>();
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
        }
    }
}
