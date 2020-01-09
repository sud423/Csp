using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace Csp.Wx.Api
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
                    webBuilder
                    //.ConfigureKestrel(serverOptions =>
                    //{
                    //    serverOptions.Listen(IPAddress.Loopback, 65311);
                    //    //serverOptions.Listen(IPAddress.Loopback, 5001,
                    //    //    listenOptions =>
                    //    //    {
                    //    //        listenOptions.UseHttps("testCert.pfx",
                    //    //            "testPassword");
                    //    //    });
                    //})
                    .UseStartup<Startup>();
                });
    }
}
