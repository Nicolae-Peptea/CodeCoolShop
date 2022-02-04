using DataAccessLayer;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Extensions
{
    public static class SeedData
    {
       
        public static async Task TrySeedData(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<CodeCoolShopContext>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                await context.Database.MigrateAsync();
                await Seed.SeedData(context, userManager);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An error occured during migration");
            }
        }
    }
}
