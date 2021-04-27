using Domain.Accounts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services;
using Services.Accounts;
using Storage;
using System;
using System.Collections.Generic;
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
                var stores = hostContext.Configuration
                                .GetSection(nameof(IStorage))
                                .GetChildren()
                                .Select(s =>
                                {
                                    var storageTypeString = s.Get<StorageConfig>().Type;
                                    var storeType = typeof(IStorage).Load(storageTypeString);
                                    var configType = typeof(IStorage).Load($"{storageTypeString}Config");
                                    var configurationEntities = s.GetChildren().ToList().FirstOrDefault(w => w.Key == "Entities");
                                    var t = configurationEntities.GetChildren()
                                                .Select(s2 => s2.Get(configType))
                                                .ToDictionary(
                                                    x => x.GetValue<string>("Entity"),
                                                    y => Activator.CreateInstance(storeType, y) as IStorage);
                                    return new KeyValuePair<string, Dictionary<string, IStorage>>(storageTypeString, t);
                                })
                                .ToList();

                //IStorage
                //IStorage[] storageFactory(IServiceProvider sp) => {
                //    return null
                //};

                //services.AddSingleton<IStorage[], IStorage[]>(storageFactory);
                services.AddSingleton<Services<Account>, AccountServices>();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
            });
    }
}