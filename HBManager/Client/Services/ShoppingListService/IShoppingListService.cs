namespace HBManager.Client
{
    public interface IShoppingListService
    {
        Task<ServiceResponse<ShoppingList>> GetShoppingListByIDAsync(int id);
        Task<ServiceResponse<List<string>>> GetMostUsedProductTitlesAsync(string searchString);
        Task<ServiceResponse<List<ProductSearchDTO>>> GetMostUsedProductsAsync(string searchString);
        Task<ServiceResponse<bool>> DeleteProduct(int productID);
        Task<ServiceResponse<bool>> DeleteShoppingList(int shoppingListID);
        Task<ServiceResponse<Product?>> EditProduct(ProductEditDTO productDTO);
        Task<ServiceResponse<Product?>> AddProduct(ProductAddDTO productDTO);
        Task<ServiceResponse<ShoppingList?>> AddShoppingList(ShoppingListAddDTO shoppingListDTO);
        Task<ServiceResponse<ShoppingList?>> EditShoppingList(ShoppingListEditDTO shoppingListDTO);
        Task<ServiceResponse<List<ProductSearchDTO>>> GetProductSearchItemsAsync(string searchString);
    }
}
