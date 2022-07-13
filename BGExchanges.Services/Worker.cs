using BGExchanges.Services.External;
using Serilog;

namespace BGExchanges.Services
{
    public class Worker : BackgroundService
    {
        private int _executionCount = 0;
        private readonly TimeSpan _period = TimeSpan.FromMinutes(30);
        private readonly ILogger<Worker> _logger;
        private readonly CoinApiService _coinService;

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _coinService = new CoinApiService(config);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);

            while (!stoppingToken.IsCancellationRequested 
                && await timer.WaitForNextTickAsync(stoppingToken))
            {

                var cotacoesReal = await _coinService.PegarCotacoes("BRL", stoppingToken);
                if (cotacoesReal == null || !cotacoesReal.rates.Any())
                    continue;

                var result = string.Join(Environment.NewLine, cotacoesReal.rates.Select(rate =>
                    $"A cotação da moeda 'BRL' para {rate.asset_id_quote} é de {rate.rate} em {rate.time.ToString("dd/MM/yyy 'às' HH:mm:ss")}"
                ));

                Log.Logger.Information(result);

                _executionCount++;
                Log.Logger.Information($"Worker running at: {DateTimeOffset.Now.ToString("ddMMyyyy-HH:mm:ss")} by the {_executionCount}º time.");
            }
        }
    }
}