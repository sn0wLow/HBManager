using HBManager.Shared;
using System.Net.Http.Json;

namespace HBManager.Client
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly HttpClient http;

        public ShoppingListService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<ServiceResponse<Product?>> AddProduct(ProductAddDTO productDTO)
        {
            var response = await http.PostAsJsonAsync($"api/ShoppingList/product/", productDTO);
            var result = (await response.Content.ReadFromJsonAsync<ServiceResponse<Product?>>());

            return result!;
        }



        public async Task<ServiceResponse<Product?>> EditProduct(ProductEditDTO productDTO)
        {
            var response = await http.PutAsJsonAsync($"api/ShoppingList/product/", productDTO);
            var result = (await response.Content.ReadFromJsonAsync<ServiceResponse<Product>>());

            return result!;
        }

        public async Task<ServiceResponse<bool>> DeleteProduct(int productID)
        {
            var response = await http.DeleteAsync($"api/ShoppingList/product/{productID}/");
            var result = (await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>());

            return result!;
        }

        public async Task<ServiceResponse<bool>> DeleteShoppingList(int shoppingListID)
        {
            var response = await http.DeleteAsync($"api/ShoppingList/{shoppingListID}/");
            var result = (await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>());

            return result!;
        }

        public async Task<ServiceResponse<ShoppingList>> GetShoppingListByIDAsync(int id)
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<ShoppingList>>
                ($"api/ShoppingList/{id}");
            return result!;
        }

        public async Task<ServiceResponse<ShoppingList?>> AddShoppingList(ShoppingListAddDTO shoppingListDTO)
        {
            var response = await http.PostAsJsonAsync($"api/ShoppingList/", shoppingListDTO);
            var result = (await response.Content.ReadFromJsonAsync<ServiceResponse<ShoppingList?>>());

            return result!;
        }

        public async Task<ServiceResponse<ShoppingList?>> EditShoppingList(ShoppingListEditDTO shoppingListDTO)
        {
            var response = await http.PutAsJsonAsync($"api/ShoppingList/", shoppingListDTO);
            var result = (await response.Content.ReadFromJsonAsync<ServiceResponse<ShoppingList>>());

            return result!;
        }

        public async Task<ServiceResponse<List<string>>> GetMostUsedProductTitlesAsync(string searchString)
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/ShoppingList/product/titles?searchString={searchString}");
            return result!;
        }

        public async Task<ServiceResponse<List<ProductSearchDTO>>> GetMostUsedProductsAsync(string searchString)
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<List<ProductSearchDTO>>>($"api/ShoppingList/product/mostUsedProducts?searchString={searchString}");
            return result!;
        }

        public async Task<ServiceResponse<List<ProductSearchDTO>>> GetProductSearchItemsAsync(string searchString)
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<List<ProductSearchDTO>>>($"api/ShoppingList/product/search?searchString={searchString}");
            return result!;
        }
    }
}
