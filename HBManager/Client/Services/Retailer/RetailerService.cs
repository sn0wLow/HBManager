using HBManager.Shared;
using System.Net.Http.Json;

namespace HBManager.Client
{
    public class RetailerService : IRetailerService
    {
        private readonly HttpClient http;

        public RetailerService(HttpClient http)
        {
            this.http = http;
        }
        public async Task GetRetailersAsync()
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<List<Retailer>>>("api/Retailer/");

            if (result is not null && result.Data is not null)
            {
                Retailers = result.Data;
            }
        }

        public async Task<ServiceResponse<Retailer?>> GetRetailerByIDAsync(int id)
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<Retailer?>>($"api/Retailer/{id}");
            return result!;
        }

        public async Task<ServiceResponse<List<Retailer>>> GetRetailersWithProductInfoAsync()
        {

            var result = await http.GetFromJsonAsync<ServiceResponse<List<Retailer>>>("api/Retailer/with-product-info");
            return result!;
        }

        public List<Retailer> Retailers { get; set; } = new List<Retailer>();
    }
}
