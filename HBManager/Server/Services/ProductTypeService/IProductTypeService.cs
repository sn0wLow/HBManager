namespace HBManager.Server
{
    public interface IProductTypeService
    {
        Task<ServiceResponse<List<ProductType>>> GetProductTypesAsync();
    }
}
