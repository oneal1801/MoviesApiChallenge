
using Serilog.Events;
using Serilog.Templates;
using Serilog;
using Serilog.Templates.Themes;
using MoviesApiChallenge.Utilities;

namespace MoviesApiChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console(new ExpressionTemplate("[{@t:yyyy-MM-dd HH:mm:ss.fff} {@l:u4}] {@m}\n{@x}"))
            .CreateBootstrapLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .Enrich.With(new OperationIdEnricher())
                    .WriteTo.Console(new ExpressionTemplate("[{@t:yyyy-MM-dd HH:mm:ss.fff} {ElementAt(@p, 'RequestId') ci} {@l:u4}] {@m}\n{@x}", theme: TemplateTheme.Literate)))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:5000");
                });
    }
}