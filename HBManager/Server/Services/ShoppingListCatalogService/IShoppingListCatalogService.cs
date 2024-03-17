namespace HBManager.Server
{
    public interface IShoppingListCatalogService
    {
        Task<ServiceResponse<List<ShoppingListCatalog>>> GetShoppingListCatalogsAsync();
        Task<ServiceResponse<List<ShoppingListCatalog>>> GetShoppingListCatalogsByYearAsync(int year);
        Task<ServiceResponse<ShoppingListCatalog>> GetShoppingListCatalogByYearMonthAsync(int year, int month);
        Task<ServiceResponse<ShoppingListCatalog?>> AddShoppingListCatalog(ShoppingListCatalogAddDTO shoppingListCatalogDTO);
        Task<ServiceResponse<bool>> DeleteShoppingListCatalog(int shoppingListCatalogID);
        Task<ServiceResponse<ShoppingListCatalog?>> EditShoppingListCatalog(ShoppingListCatalogEditDTO shoppingListCatalogDTO);
    }
}
