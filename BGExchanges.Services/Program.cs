using BGExchanges.Services;
using Serilog;

string rootFolder = @"C:\\Users\\hedga\\source\\repos\\BGExchanges\\BGExchanges.Services";

Log.Logger = new LoggerConfiguration()
                       .Enrich.FromLogContext()
                       .WriteTo.File(rootFolder + "\\Log\\log_.txt", rollingInterval: RollingInterval.Minute)
                       .WriteTo.Console()
                       .CreateLogger();

Log.Information("Iniciando o processo de build...");

Log.Information("Carregando arquivo de configuração e variaveis de ambiente...");

var configuration = new ConfigurationBuilder()
   .SetBasePath(rootFolder)
   .AddEnvironmentVariables()
   .AddJsonFile("appsettings.json", false)
   .Build();


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(c =>
    {
        c.AddSerilog(Log.Logger);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();        
    })
    .Build();

await host.RunAsync();
