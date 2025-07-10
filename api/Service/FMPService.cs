using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Newtonsoft.Json;

namespace api.Service
{
    public class FMPService : IFMPService
    {
        private HttpClient _httpClient;
        private readonly IConfiguration _config;

        public FMPService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                var apiKey = _config["FMPKey"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new InvalidOperationException("FMPKey is missing from configuration.");
                }
                    var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/stable/profile?symbol={symbol}&apikey={apiKey}");
                
                Console.WriteLine("1");
                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine("2");
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var stock = tasks[0];
                    if (stock != null)
                    {
                        Console.WriteLine("3");
                        return stock.ToStockFromFMP();
                    }

                    return null;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}