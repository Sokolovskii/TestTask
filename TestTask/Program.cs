using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Models;
using Microsoft.Extensions.Hosting;
using NLog;

namespace TestTask
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
			var host = CreateWebHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					logger.Debug("Начало работы");
					var context = services.GetRequiredService<PersonContext>();
				}
				catch (Exception ex)
				{
					logger.Error(ex, "Stopped program because of exception");
				}
				finally
				{
					LogManager.Shutdown(); 
				}
			}

			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
		   WebHost.CreateDefaultBuilder(args)
			   .UseStartup<Startup>()
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
				})
				.UseNLog();
	}
}
