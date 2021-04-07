using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Timweb.Api
{
    [UsedImplicitly]
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            Console.WriteLine("Start");
            CallTimer();
            Console.WriteLine("Finish");
        }

        private int Foo(int foo)
        {
            return foo * 2;
        }
        
        private int Bar(Func<int, int> multiplier, int val) => multiplier(val);

        public int Zoo()
        {
            return Bar(Foo, 20); 
        }   

        
        private static async Task CallTimer()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("BeforeSleep");
                Thread.Sleep(3000);
                Console.WriteLine("AfterSleep");
            });
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSentry();
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging((c, l) =>
                {
                    l.AddConfiguration(c.Configuration);
                    l.AddSentry();
                });
        }
    }
}