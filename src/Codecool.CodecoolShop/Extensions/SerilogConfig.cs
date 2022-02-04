using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Extensions
{
    public static class SerilogConfig
    {
        public static void Configure()
        {
            var confirguration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            Log.Logger = new LoggerConfiguration().
                ReadFrom.Configuration(confirguration)
                .CreateLogger();
        }
    }
}
