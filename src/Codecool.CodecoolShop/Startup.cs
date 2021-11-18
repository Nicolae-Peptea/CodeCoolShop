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
using Microsoft.AspNetCore.Http;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;
using Serilog;
using Stripe;
using System.Text.Json.Serialization;

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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IProductDao, ProductDaoDb>();
            services.AddTransient<IProductCategoryDao, ProductCategoryDaoDb>();
            services.AddTransient<ISupplierDao, SupplierDaoDb>();
            services.AddTransient<IOrderDao, OrderDaoDb>();
            services.AddTransient<IProductOrderDao, ProductOrderDaoDb>();
            services.AddTransient<ICustomerDao, CustomerDaoDb>();
            services.AddTransient<IProductOrderDao, ProductOrderDaoDb>();

            services.AddScoped<IProductServicesDb, ProductServices>();
            services.AddScoped<ICategoryService, CategoryServices>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<ICustomerService, CustomerServices>();
            services.AddScoped<IProductOrderServices, ProductOrderServices>();

            services.AddScoped<IMailService, MailServices>();

            services.AddControllersWithViews();

            string connectionString = Configuration.GetConnectionString("CodeCoolShop");
            services.AddDbContext<CodeCoolShopContext>(options =>
                options.UseSqlServer(connectionString),
                ServiceLifetime.Transient
            );

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<CodeCoolShopContext>();
            
            services.AddReact();

            services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
              .AddV8();

            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.Configure<SendgridSettings>(Configuration.GetSection("Sendgrid"));
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseReact(config => config.AddScript("~/js/app.jsx"));
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=HomePage}/{action=Index}/{id?}");
            });
        }
    }
}
