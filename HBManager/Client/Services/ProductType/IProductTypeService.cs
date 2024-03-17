namespace HBManager.Client
{
    public interface IProductTypeService
    {
        List<ProductType> ProductTypes { get; set; }
        Task GetProductTypesAsync();
    }
}
