using Domain.Accounts;
using Domain.Balances;
using Domain.Transactions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services;
using Services.Accounts;
using Services.Balances;
using Services.Transactions;
using Storage;
using System;
using System.Linq;
using Utils;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureServices((hostContext, services) =>
            {
                IStorage[] storageFactory(IServiceProvider sp) =>
                    hostContext
                        .Configuration
                        .GetSection(nameof(IStorage))
                        .GetChildren()
                        .Select(s =>    // s is every element in the array
                        {
                            var storageTypeString = s.Get<StorageConfig>().Type;
                            var storageType = typeof(IStorage).Load(storageTypeString);
                            var storageConfigType = typeof(IStorage).Load($"{storageTypeString}Config");
                            var storageConfig = s.GetChildren().ToList().SingleOrDefault(w => w.Key == "Config").Get(storageConfigType);
                            return Activator.CreateInstance(storageType, storageConfig) as IStorage;
                        })
                        .ToArray();

                services.AddSingleton<IStorage[], IStorage[]>(storageFactory);

                services.AddSingleton<Services<Account>, AccountServices>();
                services.AddSingleton<Services<MonthlyBalance>, MonthlyBalanceServices>();
                services.AddSingleton<Services<Expense>, ExpenseServices>();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
            });
    }
}