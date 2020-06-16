using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGMLD3.Utils;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Configuration;
using Serilog.Sinks.Elasticsearch;

namespace DGMLD3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = null;
            try
            {
                configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                 .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri($"{Secrets.GetConnectionString(configuration, "Log_ElasticIndexBaseUrl")}"))
                 {
                     AutoRegisterTemplate = true,
                     ModifyConnectionSettings = x => x.BasicAuthentication(Secrets.GetAppSettingsValue(configuration, "elastic_name"), Secrets.GetAppSettingsValue(configuration, "elastic_pasword")),
                     AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                     IndexFormat = $"{Secrets.GetAppSettingsValue(configuration, "AppName")}" + "-{0:yyyy.MM}"
                 })
                .CreateLogger();

            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .UseSerilog()
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
    }
}
