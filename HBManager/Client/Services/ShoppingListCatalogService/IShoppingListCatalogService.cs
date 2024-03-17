namespace HBManager.Client
{
    public interface IShoppingListCatalogService
    {
        List<ShoppingListCatalog> ShoppingListCatalogs { get; set; }
        Task GetShoppingListCatalogsAsync();
        Task<ServiceResponse<List<ShoppingListCatalog>>> GetShoppingListCatalogsByYearAsync(int year);
        Task<ServiceResponse<ShoppingListCatalog>> GetShoppingListCatalogByYearMonthAsync(int year, int month);
        Task<ServiceResponse<ShoppingListCatalog?>> AddShoppingListCatalog(ShoppingListCatalogAddDTO shoppingListCatalogDTO);
        Task<ServiceResponse<bool>> DeleteShoppingListCatalog(int shoppingListCatalogID);
        Task<ServiceResponse<ShoppingListCatalog?>> EditShoppingListCatalog(ShoppingListCatalogEditDTO shoppingListCatalogDTO);
    }
}
