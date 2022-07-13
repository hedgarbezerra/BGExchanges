using BGExchanges.Services.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGExchanges.Services.External
{
    public class CoinApiService
    {
        private readonly string _baseURL;
        private readonly string _apiKey;
        private readonly RestClient _requestClient;
        public CoinApiService(IConfiguration config)
        {
            var apiConfig = config.GetSection("CoinAPI");

            _apiKey = apiConfig["Key"];
            _baseURL = apiConfig["Url"];
            _requestClient = new RestClient(_baseURL);
            _requestClient.AddDefaultHeader("X-CoinAPI-Key", _apiKey);
        }

        public async Task<ExchangeRates> PegarCotacoes(string idMoeda, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = new RestRequest(@$"/exchangerate/{idMoeda}", Method.Get);

            var response = await _requestClient.ExecuteAsync<ExchangeRates>(request, cancellationToken);

            return (response != null && response.IsSuccessful) ? response.Data : throw new Exception($"Something wrong at {_baseURL}");
        }

        public async Task<ExchangeRate> PegarCotacao(string idMoedaDe, string idMoedaPara, DateTime data, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = new RestRequest(@$"/exchangerate/{idMoedaDe}/{idMoedaPara}?time={data}", Method.Get);

            var response = await _requestClient.ExecuteAsync<ExchangeRate>(request, cancellationToken);

            return (response != null && response.IsSuccessful) ? response.Data : throw new Exception($"Something wrong at {_baseURL}");
        }

        public async Task<IList<Asset>> ListarMoedas(string identificador = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = new RestRequest(@$"/assets/{identificador}", Method.Get);

            var response = await _requestClient.ExecuteAsync<IList<Asset>>(request, cancellationToken);

            return (response != null && response.IsSuccessful) ? response.Data : throw new Exception($"Something wrong at {_baseURL}");
        }
    }
}
