using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer
{
    public class Seed
    {
        public static async Task SeedData(CodeCoolShopContext context, UserManager<IdentityUser> userManager)
        {
            var suppliers = new List<Supplier>
            {
                new Supplier
                {
                    Id = 1,
                    Name = "Amazon",
                    Description = "Digital content and services"
                },

                new Supplier
                {
                    Id = 2,
                    Name = "Lenovo",
                    Description = "Computers"
                },
                new Supplier
                {
                    Id = 3,
                    Name = "Apple",
                    Description = "Consumer electronics, computer software, and online services."
                },
                new Supplier
                {
                    Id = 4,
                    Name = "Xiaomi",
                    Description = "Consumer electronics, computer software, and online services."
                },
                new Supplier
                {
                    Id = 5,
                    Name = "Samsung",
                    Description = "Consumer electronics, computer software, and online services."
                },
                new Supplier
                {
                    Id = 6,
                    Name = "Motorola",
                    Description = "Consumer electronics."
                },
            };

            await context.Suppliers.AddRangeAsync(suppliers);


            var categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Tablet",
                    Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display."
                },
                new Category
                {
                    Id = 2,
                    Name = "Phone",
                    Description = "A mobile phone, cellular phone, cell phone, cellphone, handphone, or hand phone, sometimes shortened to simply mobile, cell or just phone."
                },
            };

            await context.Categories.AddRangeAsync(categories);

            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Amazon Fire",
                    Price = 49.9m,
                    Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.",
                    CategoryId = 1,
                    SupplierId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Lenovo IdeaPad Miix 700",
                    Price = 479.0m,
                    Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.",
                    CategoryId = 1,
                    SupplierId = 2
                },
                new Product
                {
                    Id = 3,
                    Name = "Amazon Fire HD 8",
                    Price = 89.0m,
                    Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.",
                    CategoryId = 1,
                    SupplierId = 1
                },
                new Product
                {
                    Id = 4,
                    Name = "Apple iPhone 12 Pro Max",
                    Price = 1239.0m,
                    Description = "The iPhone 12 is a new iPhone model developed by Apple Inc. It is part of a device family that was announced during a special event on October 13, 2020 to succeed the iPhone 11 line.",
                    CategoryId = 2,
                    SupplierId = 3
                },

                new Product
                {
                    Id = 5,
                    Name = "Xiaomi Redmi Note 9 Pro",
                    Price = 199.9m,
                    Description = "Dual SIM, 128GB, 6GB RAM, 4G, Tropical Green",
                    CategoryId = 2,
                    SupplierId = 4
                },
                new Product
                {
                    Id = 6,
                    Name = "Lenovo Tab P11 Plus",
                    Price = 279.0m,
                    Description = "Octa-Core, 11 inches 2K IPS, 6GB RAM, 128GB, WiFi, Slate Grey",
                    CategoryId = 1,
                    SupplierId = 6
                },
                new Product
                {
                    Id = 7,
                    Name = "Samsung Galaxy S21",
                    Price = 809.0m,
                    Description = "Dual SIM, 128GB, 8GB RAM, 5G, Phantom Grey",
                    CategoryId = 2,
                    SupplierId = 5
                },
                new Product
                {
                    Id = 8,
                    Name = "Apple iPhone 13 Pro",
                    Price = 1159.9m,
                    Description = "128GB, 5G, Graphite",
                    CategoryId = 2,
                    SupplierId = 3
                },

                new Product
                {
                    Id = 9,
                    Name = "Xiaomi Pad 5",
                    Price = 369.9m,
                    Description = "Octa-Core, 11 inches, 6GB RAM, 128GB, Wi-Fi, Cosmic Gray",
                    CategoryId = 1,
                    SupplierId = 4
                },
                new Product
                {
                    Id = 10,
                    Name = "Motorola G60s",
                    Price = 199.8m,
                    Description = "Dual SIM, 128GB, 6GB RAM, Blue",
                    CategoryId = 2,
                    SupplierId = 6
                },
                new Product
                {
                    Id = 11,
                    Name = "Samsung Galaxy Tab A7",
                    Price = 302.9m,
                    Description = "Octa-Core, 10.4 inches, 3GB RAM, 32GB, 4G, Gray",
                    CategoryId = 1,
                    SupplierId = 5
                },
                new Product
                {
                    Id = 12,
                    Name = "Apple iPad 8 (2020)",
                    Price = 1239.0m,
                    Description = "10.2 inches, 32GB, Wi-Fi, Space Grey",
                    CategoryId = 1,
                    SupplierId = 3
                },
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}
