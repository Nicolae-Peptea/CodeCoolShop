using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccessLayer.Data
{
    public partial class CodeCoolShopContext : IdentityDbContext
    {
        public CodeCoolShopContext(DbContextOptions<CodeCoolShopContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ProductOrder>().HasKey(t => new { t.ProductId, t.OrderId });
            Supplier amazon = new Supplier { Id = 1, Name = "Amazon", Description = "Digital content and services" };
            Supplier lenovo = new Supplier { Id = 2, Name = "Lenovo", Description = "Computers" };
            Supplier apple = new Supplier { Id = 3, Name = "Apple", Description = "Consumer electronics, computer software, and online services." };

            Category tablet = new Category { Id = 1, Name = "Tablet", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            Category phone = new Category { Id = 2, Name = "Phone", Description = "A mobile phone, cellular phone, cell phone, cellphone, handphone, or hand phone, sometimes shortened to simply mobile, cell or just phone." };

            var pr1 = new Product { Id = 1, Name = "Amazon Fire", Price = 49.9m, Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", CategoryId = 1, SupplierId = 1 };
            var pr2 = new Product { Id = 2, Name = "Lenovo IdeaPad Miix 700", Price = 479.0m, Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", CategoryId = 1, SupplierId = 2 };
            var pr3 = new Product { Id = 3, Name = "Amazon Fire HD 8", Price = 89.0m, Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", CategoryId = 1, SupplierId = 1 };
            var pr4 = new Product { Id = 4, Name = "Apple iPhone 12 Pro Max", Price = 1239.0m, Description = "The iPhone 12 is a new iPhone model developed by Apple Inc. It is part of a device family that was announced during a special event on October 13, 2020 to succeed the iPhone 11 line.", CategoryId = 2, SupplierId = 3 };

            var Ion = new Customer { Id = 1, BillingAddressCity = "Topolog", FirstName = "Ion" };
            var dummyOrder = new Order { Id = 1, CustomerId = 5, OrderPlaced = DateTime.Now };

            builder.Entity<Supplier>().HasData(amazon, lenovo, apple);
            builder.Entity<Category>().HasData(tablet, phone);
            builder.Entity<Product>().HasData(pr1, pr2, pr3, pr4);
            builder.Entity<Customer>().HasData(Ion);

        }
    }
}
