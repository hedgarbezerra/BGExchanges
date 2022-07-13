using BGExchanges.Services.External;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .Build();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Development.json")
    .AddEnvironmentVariables()
    .Build();


var service = new CoinApiService(config);

var assets = await service.ListarMoedas();
var exchange = service.PegarCotacao("", "", DateTime.Now);