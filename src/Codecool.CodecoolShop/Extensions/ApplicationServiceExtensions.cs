using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Services;
using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codecool.CodecoolShop.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationInternalDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductDao, ProductDaoDb>();
            services.AddTransient<IProductCategoryDao, ProductCategoryDaoDb>();
            services.AddTransient<ISupplierDao, SupplierDaoDb>();
            services.AddTransient<IOrderDao, OrderDaoDb>();
            services.AddTransient<IProductOrderDao, ProductOrderDaoDb>();
            services.AddTransient<ICustomerDao, CustomerDaoDb>();
            services.AddTransient<IProductOrderDao, ProductOrderDaoDb>();

            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<ICategoryService, CategoryServices>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<ICustomerService, CustomerServices>();
            services.AddScoped<IProductOrderServices, ProductOrderServices>();

            services.AddScoped<IMailService, MailServices>();

            return services;
        }

        public static IServiceCollection CongigureDbContext(this IServiceCollection services, IConfiguration config)
        {
            string connectionString = config.GetConnectionString("CodeCoolShop");
            
            services.AddDbContext<CodeCoolShopContext>(options =>
                options.UseSqlServer(connectionString),
                ServiceLifetime.Transient
            );

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<CodeCoolShopContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
