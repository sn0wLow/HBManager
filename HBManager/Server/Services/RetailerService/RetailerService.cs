using Microsoft.OpenApi.Validations;

namespace HBManager.Server
{
    public class RetailerService : IRetailerService
    {
        private readonly DataContext context;
        private readonly IAuthService authService;

        public RetailerService(DataContext context, IAuthService authService)
        {
            this.context = context;
            this.authService = authService;
        }
        public async Task<ServiceResponse<List<Retailer>>> GetRetailersAsync()
        {
            var response = new ServiceResponse<List<Retailer>>()
            {
                Data = await context.Retailers.ToListAsync(),
                Success = true
            };

            return response;
        }

        public async Task<ServiceResponse<Retailer?>> GetRetailerByIDAsync(int id)
        {
            var userID = await this.authService.GetUserID();
            var retailer = await context.Retailers.FirstOrDefaultAsync(x => x.ID == id);

            var response = new ServiceResponse<Retailer?>();

            if (retailer is null)
            {
                response.Message = "Dieser Händler existiert nicht.";
                response.Success = false;
            }
            else
            {
                var products = await this.context.ShoppingLists
                    .Include(x => x.Products)
                    .ThenInclude(x => x.ProductType)
                    .Include(x => x.Products)
                    .ThenInclude(x => x.Consumers)
                    .Where(x => x.RetailerID == retailer.ID && x.UserID == userID)
                    .SelectMany(x => x.Products)
                    .ToListAsync();

                retailer.Products = products;

                response.Data = retailer;
                response.Success = true;
            }

            return response;
        }


        public async Task<ServiceResponse<List<Retailer>>> GetRetailersWithProductInfoAsync()
        {
            var userID = await this.authService.GetUserID();
            var retailers = await context.Retailers.ToListAsync();

            var productsByRetailer = await context.ShoppingLists
                .Include(x => x.Products)
                .ThenInclude(x => x.Consumers)
                .Where(x => x.UserID == userID)
                .GroupBy(x => x.RetailerID)
                .ToListAsync();

            foreach (var grouping in productsByRetailer)
            {
                var shoppingLists = grouping.Select(x => x);

                if (shoppingLists.Any(x => x.Products.Any()))
                {
                    var retailer = retailers.Find(x => x.ID == grouping.Key)!;

                    retailer.HasAnyProducts = true;
                    retailer.TotalVisits = shoppingLists.Count();
                    retailer.TotalProductAmount = shoppingLists.Sum(x => x.Products.Count);
                    retailer.TotalProductPrice = (double)shoppingLists.Sum(x => x.Products.Sum(x => (x.Price * x.Quantity)));
                    retailer.AverageProductPrice = (double)shoppingLists.Average(x => x.Products.Average(x => x.Price));
                }
            }


            var response = new ServiceResponse<List<Retailer>>()
            {
                Data = retailers,
                Success = true
            };

            return response;
        }
    }
}
