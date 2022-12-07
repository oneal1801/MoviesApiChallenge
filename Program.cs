
using Serilog.Events;
using Serilog.Templates;
using Serilog;
using Serilog.Templates.Themes;
using MoviesApiChallenge.Utilities;
using MoviesApiChallenge;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console(new ExpressionTemplate("[{@t:yyyy-MM-dd HH:mm:ss.fff} {@l:u4}] {@m}\n{@x}"))
            .CreateBootstrapLogger();

try {
    var builder = WebApplication.CreateBuilder(args);
    
    Log.Information("Starting web host");
    builder.Host.UseSerilog((context, services, configuration) => configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext()
                        .Enrich.With(new OperationIdEnricher())
                        .WriteTo.Console(new ExpressionTemplate("[{@t:yyyy-MM-dd HH:mm:ss.fff} {ElementAt(@p, 'RequestId') ci} {@l:u4}] {@m}\n{@x}", theme: TemplateTheme.Literate)));
    builder.WebHost.UseUrls("http://*:80");
    var startup = new Startup(builder.Configuration);
    startup.ConfigureServices(builder.Services);

    var app = builder.Build();

    startup.Configure(app, app.Environment);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}