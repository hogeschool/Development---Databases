using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;

namespace Forum
{
  public class EntryPoint
  {
    public static void Main(string[] args)
    {
      var server = CreateWebHostBuilder(args).Build();
      var app = new CommandLineApplication();

      app.OnExecute(() =>
      {
        server.Run();
        return 0;
      });

      app.Execute(args);
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
          .ConfigureAppConfiguration((ctx, config) =>
              config.SetBasePath(ctx.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true)
                .AddCommandLine(args)
                .AddEnvironmentVariables()
              )
          .UseStartup<Startup>()
          .UseUrls("http://*:5000");
    }
  }
}


