namespace HBManager.Server
{
    public class ShoppingListCatalogService : IShoppingListCatalogService
    {
        private readonly DataContext context;
        private readonly IAuthService authService;

        public ShoppingListCatalogService(DataContext context, IAuthService authService)
        {
            this.context = context;
            this.authService = authService;
        }

        public async Task<ServiceResponse<List<ShoppingListCatalog>>> GetShoppingListCatalogsAsync()
        {
            var response = new ServiceResponse<List<ShoppingListCatalog>>()
            {
                Data = await context.ShoppingListCatalogs.Include(c => c.ShoppingLists).ThenInclude(s => s.Products).ToListAsync(),
                Success = true
            };

            return response;
        }

        public async Task<ServiceResponse<List<ShoppingListCatalog>>> GetShoppingListCatalogsByYearAsync(int year)
        {
            var userID = await this.authService.GetUserID();
            var response = new ServiceResponse<List<ShoppingListCatalog>>()
            {
                Data = await context.ShoppingListCatalogs
                .Where(c => c.UserID == userID && c.Date.Year == year)
                .Include(c => c.ShoppingLists)
                .ThenInclude(s => s.Products)
                .ToListAsync(),

                Success = true
            };


            return response;
        }

        public async Task<ServiceResponse<ShoppingListCatalog>> GetShoppingListCatalogByYearMonthAsync(int year, int month)
        {
            var userID = await this.authService.GetUserID();
            var response = new ServiceResponse<ShoppingListCatalog>();
            ShoppingListCatalog? catalog = null;

            catalog = await context.ShoppingListCatalogs
                       .Include(c => c.ShoppingLists)
                       .ThenInclude(s => s.Products)
                       .ThenInclude(s => s.Consumers)
                       .FirstOrDefaultAsync(x => x.UserID == userID && x.Date.Year == year && x.Date.Month == month);

            if (catalog is null)
            {
                response.Success = false;
                response.Message = "Sorry, für diesen Monat gibt es keinen Eintrag.";
            }
            else
            {
                response.Success = true;
                response.Data = catalog;
            }

            return response;

        }

        public async Task<ServiceResponse<ShoppingListCatalog?>> AddShoppingListCatalog(ShoppingListCatalogAddDTO shoppingListCatalogDTO)
        {
            var userID = await this.authService.GetUserID();

            var shoppingListCatalogDB = await this.context.ShoppingListCatalogs
                .FirstOrDefaultAsync(x => x.UserID == userID &&
                x.Date.Year == shoppingListCatalogDTO.Date.Year && x.Date.Month == shoppingListCatalogDTO.Date.Month);

            if (shoppingListCatalogDB is not null)
            {
                return new ServiceResponse<ShoppingListCatalog?>
                {
                    Success = false,
                    Data = null,
                    Message = "Ein Einkaufskatalog für dieses Datum existiert bereits."
                };
            }



            var shoppingListCatalog = new ShoppingListCatalog()
            {
                Date = shoppingListCatalogDTO.Date,
                Budget = shoppingListCatalogDTO.Budget,
                UserID = userID,
            };


            this.context.ShoppingListCatalogs.Add(shoppingListCatalog);

            await this.context.SaveChangesAsync();

            return new ServiceResponse<ShoppingListCatalog?> { Data = shoppingListCatalog, Success = true };
        }

        public async Task<ServiceResponse<bool>> DeleteShoppingListCatalog(int shoppingListCatalogID)
        {
            var userID = await this.authService.GetUserID();

            var shoppingListCatalog = await this.context.ShoppingListCatalogs
                .Include(s => s.ShoppingLists)
                .ThenInclude(s => s.Products)
                .FirstOrDefaultAsync(x => x.UserID == userID && x.ID == shoppingListCatalogID);

            if (shoppingListCatalog is null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Dieser Einkaufskatalog existiert nicht."
                };
            }

            this.context.Products.RemoveRange(this.context.ShoppingLists
                .Where(x => x.UserID == userID && x.ShoppingListCatalogID == shoppingListCatalog.ID)
                .SelectMany(x => x.Products));

            this.context.ShoppingLists.RemoveRange(this.context.ShoppingLists
                .Where(s => s.ShoppingListCatalogID == shoppingListCatalog.ID));

            this.context.ShoppingListCatalogs.Remove(shoppingListCatalog);

            await this.context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Success = true };
        }

        public async Task<ServiceResponse<ShoppingListCatalog?>> EditShoppingListCatalog(ShoppingListCatalogEditDTO shoppingListCatalogDTO)
        {
            var userID = await this.authService.GetUserID();

            var shoppingListCatalog = await this.context.ShoppingListCatalogs
                .FirstOrDefaultAsync(x => x.UserID == userID && x.ID == shoppingListCatalogDTO.ID);

            if (shoppingListCatalog is null)
            {
                return new ServiceResponse<ShoppingListCatalog?>
                {
                    Success = false,
                    Data = null,
                    Message = "Dieser Monatskatalog existiert nicht."
                };
            }

            shoppingListCatalog.Budget = shoppingListCatalogDTO.Budget;

            await context.SaveChangesAsync();

            return new ServiceResponse<ShoppingListCatalog?> { Data = shoppingListCatalog, Success = true };
        }

    }
}
