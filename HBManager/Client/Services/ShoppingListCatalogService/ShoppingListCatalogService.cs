using System.Net.Http.Json;

namespace HBManager.Client
{
    public class ShoppingListCatalogService : IShoppingListCatalogService
    {
        private readonly HttpClient http;

        public ShoppingListCatalogService(HttpClient http)
        {
            this.http = http;
        }

        public async Task GetShoppingListCatalogsAsync()
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<List<ShoppingListCatalog>>>("api/ShoppingListCatalog");

            if (result is not null && result.Data is not null)
            {
                ShoppingListCatalogs = result.Data;
            }
        }

        public async Task<ServiceResponse<List<ShoppingListCatalog>>> GetShoppingListCatalogsByYearAsync(int year)
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<List<ShoppingListCatalog>>>($"api/ShoppingListCatalog/{year}");
            return result!;
        }

        public async Task<ServiceResponse<ShoppingListCatalog>> GetShoppingListCatalogByYearMonthAsync(int year, int month)
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<ShoppingListCatalog>>($"api/ShoppingListCatalog/{year}/{month}");
            return result!;
        }

        public async Task<ServiceResponse<ShoppingListCatalog?>> AddShoppingListCatalog(ShoppingListCatalogAddDTO shoppingListCatalogDTO)
        {
            var response = await http.PostAsJsonAsync($"api/ShoppingListCatalog/", shoppingListCatalogDTO);
            var result = (await response.Content.ReadFromJsonAsync<ServiceResponse<ShoppingListCatalog?>>());

            return result!;
        }

        public async Task<ServiceResponse<bool>> DeleteShoppingListCatalog(int shoppingListCatalogID)
        {
            var response = await http.DeleteAsync($"api/ShoppingListCatalog/{shoppingListCatalogID}/");
            var result = (await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>());

            return result!;
        }

        public async Task<ServiceResponse<ShoppingListCatalog?>> EditShoppingListCatalog(ShoppingListCatalogEditDTO shoppingListCatalogDTO)
        {
            var response = await http.PutAsJsonAsync($"api/ShoppingListCatalog/", shoppingListCatalogDTO);
            var result = (await response.Content.ReadFromJsonAsync<ServiceResponse<ShoppingListCatalog>>());

            return result!;
        }

        public List<ShoppingListCatalog> ShoppingListCatalogs { get; set; } = new List<ShoppingListCatalog>();
    }
}
