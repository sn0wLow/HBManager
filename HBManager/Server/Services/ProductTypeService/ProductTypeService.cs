namespace HBManager.Server
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly DataContext context;

        public ProductTypeService(DataContext context)
        {
            this.context = context;
        }
        public async Task<ServiceResponse<List<ProductType>>> GetProductTypesAsync()
        {
            var response = new ServiceResponse<List<ProductType>>()
            {
                Data = await context.ProductTypes.OrderBy(x => x.ID == 512 ? 1 : 0).ThenBy(x => x.Name).ToListAsync(),
                Success = true
            };

            return response;
        }
    }
}
