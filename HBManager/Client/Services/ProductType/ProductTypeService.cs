using System.Net.Http.Json;

namespace HBManager.Client
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly HttpClient http;

        public ProductTypeService(HttpClient http)
        {
            this.http = http;
        }
        public async Task GetProductTypesAsync()
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<List<ProductType>>>("api/ProductType/");

            if (result is not null && result.Data is not null)
            {
                ProductTypes = result.Data;
            }
        }

        public List<ProductType> ProductTypes { get; set; } = new List<ProductType>();
    }
}
