using HBManager.Shared;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HBManager.Server
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly DataContext context;
        private readonly IAuthService authService;

        public ShoppingListService(DataContext context, IAuthService authService)
        {
            this.context = context;
            this.authService = authService;
        }

        public async Task<ServiceResponse<ShoppingList>> GetShoppingListByIDAsync(int id)
        {
            var response = new ServiceResponse<ShoppingList>();
            ShoppingList? shoppingList = null;

            var userID = await this.authService.GetUserID();

            shoppingList = await this.context.ShoppingLists
                       .Include(s => s.Retailer)
                       .Include(s => s.Products)
                       .ThenInclude(p => p.ProductType)
                       .Include(s => s.Products)
                       .ThenInclude(p => p.Consumers)
                       .FirstOrDefaultAsync(x => x.ID == id && x.UserID == userID);



            if (shoppingList is null)
            {
                response.Success = false;
                response.Message = "Für diese Einkaufsliste existiert kein Eintrag.";
            }
            else
            {
                response.Success = true;
                response.Data = shoppingList;
            }



            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteProduct(int productID)
        {
            var userID = await this.authService.GetUserID();

            var product = await this.context.Products
                    .FirstOrDefaultAsync(x => x.ID == productID && x.UserID == userID);

            if (product is null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Dieses Produkt existiert nicht."
                };
            }


            this.context.Products.Remove(product);
            await this.context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Success = true };
        }

        public async Task<ServiceResponse<Product?>> EditProduct(ProductEditDTO productDTO)
        {
            var userID = await this.authService.GetUserID();

            var product = await this.context.Products
                .Include(x => x.Consumers)
                .FirstOrDefaultAsync(x => x.UserID == userID && x.ID == productDTO.ID);

            if (product is null)
            {
                return new ServiceResponse<Product?>
                {
                    Success = false,
                    Data = null,
                    Message = "Dieses Produkt existiert nicht."
                };
            }

            var productType = await this.context.ProductTypes
                .FirstOrDefaultAsync(x => x.ID == productDTO.ProductTypeID);

            if (productType is null)
            {
                return new ServiceResponse<Product?>
                {
                    Success = false,
                    Data = null,
                    Message = "Diese Produktkategorie existiert nicht."
                };
            }

            if (productDTO.ConsumerIDs.Count > 12)
            {
                return new ServiceResponse<Product?>
                {
                    Success = false,
                    Data = null,
                    Message = "Es können max. 12 Verbraucher ausgewählt werden."
                };
            }

            List<Consumer> consumers = new List<Consumer>();

            foreach (var consumerID in productDTO.ConsumerIDs)
            {
                Console.WriteLine(consumerID);
                var consumer = await this.context.Consumers.FirstOrDefaultAsync(x => x.UserID == userID && x.ID == consumerID);

                if (consumer is null)
                {
                    return new ServiceResponse<Product?>
                    {
                        Success = false,
                        Data = null,
                        Message = "Mindestens ein ausgewählter Verbraucher existieret nicht."
                    };
                }
                else
                {
                    consumers.Add(consumer);
                }
            }



            product.Title = productDTO.Title;
            product.Price = productDTO.Price;
            product.Note = productDTO.Note;
            product.Quantity = productDTO.Quantity;
            product.ProductTypeID = productDTO.ProductTypeID;
            product.Consumers = consumers;

            await context.SaveChangesAsync();

            return new ServiceResponse<Product?> { Data = product, Success = true };
        }

        public async Task<ServiceResponse<Product?>> AddProduct(ProductAddDTO productDTO)
        {
            var userID = await this.authService.GetUserID();

            var shoppingList = await this.context.ShoppingLists
                .FirstOrDefaultAsync(x => x.UserID == userID && x.ID == productDTO.ShoppingListID);

            if (shoppingList is null)
            {
                return new ServiceResponse<Product?>
                {
                    Success = false,
                    Data = null,
                    Message = "Dieser Einkauf existiert nicht."
                };
            }

            var productType = await this.context.ProductTypes
                .FirstOrDefaultAsync(x => x.ID == productDTO.ProductTypeID);

            if (productType is null)
            {
                return new ServiceResponse<Product?>
                {
                    Success = false,
                    Data = null,
                    Message = "Diese Produktkategorie existiert nicht."
                };
            }

            if (productDTO.ConsumerIDs.Count > 12)
            {
                return new ServiceResponse<Product?>
                {
                    Success = false,
                    Data = null,
                    Message = "Es können max. 12 Verbraucher ausgewählt werden."
                };
            }


            List<Consumer> consumers = new List<Consumer>();

            foreach (var consumerID in productDTO.ConsumerIDs)
            {
                var consumer = await this.context.Consumers.FirstOrDefaultAsync(x => x.ID == consumerID);

                if (consumer is null)
                {
                    return new ServiceResponse<Product?>
                    {
                        Success = false,
                        Data = null,
                        Message = "Mindestens ein ausgewählter Verbraucher existieret nicht."
                    };
                }
                else
                {
                    consumers.Add(consumer);
                }
            }

            var product = new Product()
            {
                ShoppingListID = productDTO.ShoppingListID,
                UserID = userID,
                Title = productDTO.Title,
                Price = productDTO.Price,
                Note = productDTO.Note,
                Quantity = productDTO.Quantity,
                ProductTypeID = (int)productDTO.ProductTypeID!,
                Consumers = consumers
            };


            this.context.Products.Add(product);

            await this.context.SaveChangesAsync();

            return new ServiceResponse<Product?> { Data = product, Success = true };
        }

        public async Task<ServiceResponse<bool>> DeleteShoppingList(int shoppingListID)
        {
            var userID = await this.authService.GetUserID();

            var shoppingList = await this.context.ShoppingLists
                .FirstOrDefaultAsync(x => x.UserID == userID && x.ID == shoppingListID);

            if (shoppingList is null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Dieser Einkauf existiert nicht."
                };
            }

            var shoppingListCatalog = await this.context.ShoppingListCatalogs
                .FirstOrDefaultAsync(x => x.UserID == userID && x.ID == shoppingList.ShoppingListCatalogID);

            if (shoppingListCatalog is null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Dieser Einkaufskatalog existiert nicht."
                };
            }




            this.context.Products.RemoveRange(this.context.Products
                .Where(p => p.ShoppingListID == shoppingList.ID));

            this.context.ShoppingLists.Remove(await this.context.ShoppingLists.
                FirstAsync(c => c.ID == shoppingList.ID));

            await this.context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Success = true };
        }

        public async Task<ServiceResponse<ShoppingList?>> AddShoppingList(ShoppingListAddDTO shoppingListDTO)
        {
            var userID = await this.authService.GetUserID();

            var shoppingListCatalog = await this.context.ShoppingListCatalogs
                .FirstOrDefaultAsync(x => x.UserID == userID && x.ID == shoppingListDTO.ShoppingListCatalogID);

            if (shoppingListCatalog is null)
            {
                return new ServiceResponse<ShoppingList?>
                {
                    Success = false,
                    Data = null,
                    Message = "Dieser Einkaufskatalog existiert nicht."
                };
            }

            if (shoppingListCatalog.Date.Year != shoppingListDTO.Date.Year || shoppingListCatalog.Date.Month != shoppingListDTO.Date.Month)
            {
                return new ServiceResponse<ShoppingList?>
                {
                    Success = false,
                    Data = null,
                    Message = "Einkaufsdatum passt nicht zum Einkaufkatalogsdatum."
                };
            }

            var retailer = await this.context.Retailers
                    .FirstOrDefaultAsync(x => x.ID == shoppingListDTO.RetailerID);

            if (retailer is null)
            {
                return new ServiceResponse<ShoppingList?>
                {
                    Success = false,
                    Data = null,
                    Message = "Dieser Händler existiert nicht."
                };
            }

            var shoppingList = new ShoppingList()
            {
                Date = shoppingListDTO.Date,
                RetailerID = shoppingListDTO.RetailerID,
                UserID = userID,
                ShoppingListCatalogID = shoppingListDTO.ShoppingListCatalogID
            };


            this.context.ShoppingLists.Add(shoppingList);

            await this.context.SaveChangesAsync();

            return new ServiceResponse<ShoppingList?> { Data = shoppingList, Success = true };
        }

        public async Task<ServiceResponse<ShoppingList?>> EditShoppingList(ShoppingListEditDTO shoppingListDTO)
        {
            var userID = await this.authService.GetUserID();

            var shoppingList = await this.context.ShoppingLists
                .FirstOrDefaultAsync(x => x.UserID == userID && x.ID == shoppingListDTO.ID);

            if (shoppingList is null)
            {
                return new ServiceResponse<ShoppingList?>
                {
                    Success = false,
                    Data = null,
                    Message = "Dieser Einkauf existiert nicht."
                };
            }

            var shoppingListCatalog = await this.context.ShoppingListCatalogs
                .FirstOrDefaultAsync(x => x.UserID == userID && x.ID == shoppingList.ShoppingListCatalogID);

            if (shoppingListCatalog!.Date.Year != shoppingListDTO.Date.Year || shoppingListCatalog.Date.Month != shoppingListDTO.Date.Month)
            {
                return new ServiceResponse<ShoppingList?>
                {
                    Success = false,
                    Data = null,
                    Message = "Einkaufsdatum passt nicht zum Einkaufkatalogsdatum."
                };
            }

            var retailer = await this.context.Retailers
                .FirstOrDefaultAsync(x => x.ID == shoppingListDTO.RetailerID);

            if (retailer is null)
            {
                return new ServiceResponse<ShoppingList?>
                {
                    Success = false,
                    Data = null,
                    Message = "Dieser Händler existiert nicht."
                };
            }

            shoppingList.RetailerID = shoppingListDTO.RetailerID;
            shoppingList.Date = shoppingListDTO.Date;


            await context.SaveChangesAsync();

            return new ServiceResponse<ShoppingList?> { Data = shoppingList, Success = true };
        }

        public async Task<ServiceResponse<List<ProductSearchDTO>>> GetMostUsedProductsAsync(string searchString)
        {
            var userID = await this.authService.GetUserID();

            var products = await this.context.Products
                .Where(x => x.UserID == userID)
                .Include(x => x.ProductType)
                .ToListAsync();

            var topFiveProducts = products
                .Where(x => x.UserID == userID && x.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .GroupBy(x => x.Title)
                .Select(group => new ProductSearchDTO()
                {
                    Title = group.Key,
                    Consumers = new(),
                    Price = group.GroupBy(p => p.Price)
                                 .OrderByDescending(g => g.Count())
                                 .Select(g => g.Key)
                                 .FirstOrDefault(),
                    ProductType = group.GroupBy(p => p.ProductType)
                             .OrderByDescending(g => g.Count())
                             .Select(g => g.Key)
                             .FirstOrDefault()
                })
                .Take(5)
                .ToList();

            return new ServiceResponse<List<ProductSearchDTO>>()
            {
                Success = true,
                Data = topFiveProducts,
            };
        }

        public async Task<ServiceResponse<List<string>>> GetMostUsedProductTitlesAsync(string searchString)
        {
            var userID = await this.authService.GetUserID();

            var products = await this.context.Products
                .Where(x => x.UserID == userID)
                .ToListAsync();

            var topFiveTitles = products
                .Where(x => x.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .GroupBy(p => p.Title)
                .Select(group => new
                {
                    Title = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(g => g.Count)
                .Select(x => x.Title)
                .Take(5)
                .ToList();

            return new ServiceResponse<List<string>>
            {
                Success = true,
                Data = topFiveTitles
            };
        }

        public async Task<ServiceResponse<List<ProductSearchDTO>>> GetProductSearchItemsAsync(string searchString)
        {
            var userID = await this.authService.GetUserID();

            var productSearchItems = new List<ProductSearchDTO>();

            var shoppingLists = await this.context.ShoppingLists
                .Where(x => x.UserID == userID)
                .Include(x => x.Products)
                .ThenInclude(x => x.Consumers)
                .Include(x => x.Products)
                .ThenInclude(x => x.ProductType)
                .Include(x => x.Retailer)
                .ToListAsync();

            shoppingLists = shoppingLists
                .Where(x => x.Products.Any(x => x.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            foreach (var shoppingList in shoppingLists)
            {
                var dateOfShopping = shoppingList.Date;

                foreach (var product in shoppingList.Products)
                {
                    if (product.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    {

                        productSearchItems.Add(new ProductSearchDTO()
                        {
                            ID = product.ID,
                            Title = product.Title,
                            Consumers = product.Consumers,
                            Price = product.Price,
                            ProductType = product.ProductType,
                            Quantity = product.Quantity,
                            ShoppingListID = shoppingList.ID,
                            DateOfShopping = dateOfShopping,
                            Retailer = shoppingList.Retailer
                        });
                    }
                }
            }


            return new ServiceResponse<List<ProductSearchDTO>>()
            {
                Success = true,
                Data = productSearchItems,
            };
        }
    }
}
