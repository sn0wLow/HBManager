using HBManager.Shared;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace HBManager.Client
{
    public class ConsumerService : IConsumerService
    {
        private readonly HttpClient http;

        public ConsumerService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<ServiceResponse<Consumer?>> AddConsumerAsync(ConsumerAddDTO consumerDTO)
        {
            var response = await http.PostAsJsonAsync($"api/Consumer/", consumerDTO);
            var result = (await response.Content.ReadFromJsonAsync<ServiceResponse<Consumer?>>());

            return result!;
        }

        public async Task<ServiceResponse<bool>> DeleteConsumerAsync(int consumerID)
        {
            var response = await http.DeleteAsync($"api/Consumer/{consumerID}/");
            var result = (await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>());

            return result!;
        }

        public async Task<ServiceResponse<Consumer?>> EditConsumerAsync(ConsumerEditDTO consumerDTO)
        {
            var response = await http.PutAsJsonAsync($"api/Consumer/", consumerDTO);
            var result = (await response.Content.ReadFromJsonAsync<ServiceResponse<Consumer?>>());

            return result!;
        }

        public async Task<ServiceResponse<Consumer?>> GetConsumerByIDAsync(int consumerID, bool includeProducts = true)
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<Consumer?>>($"api/Consumer/{consumerID}");

            if (includeProducts)
            {
                if (result is not null && result.Data is not null)
                {
                    var resultProducts = await http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/Consumer/{consumerID}/product");

                    if (resultProducts is not null && resultProducts.Data is not null)
                    {
                        result.Data.Products = resultProducts.Data;
                    }
                }
            }


            return result!;
        }

        public async Task GetConsumersAsync()
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<List<Consumer>>>("api/Consumer/");

            if (result is not null && result.Data is not null)
            {
                Consumers = result.Data;
            }

        }

        public List<Consumer> Consumers { get; set; } = new();
    }
}
