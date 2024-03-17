using Microsoft.Extensions.Hosting;

namespace HBManager.Server
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ShoppingList>()
            //    .HasKey(s => new { s.ID, s.ShoppingListCatalogID });

            //modelBuilder.Entity<Product>()
            //    .HasKey(s => new { s.ID, s.ShoppingListID, s.ShoppingListCatalogID });

            //modelBuilder.Entity<ShoppingList>()
            //    .HasOne(p => p.ShoppingListCatalog)
            //    .WithMany(sl => sl.ShoppingLists)
            //    .HasForeignKey(p => new { p.ShoppingListCatalogID });

            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.ShoppingList)
            //    .WithMany(sl => sl.Products)
            //    .HasForeignKey(p => new { p.ShoppingListCatalogID, p.ShoppingListID });


            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    ID = 1,
                    Name = "helmut",
                    PasswordHash = "$2a$11$cC2HGDVdEjiqn0i/zFD16.phjapeXJDtQo0LlcoEmI5/aGYkEdCVq",
                    Role = "User",
                    CreationDate = DateTime.Now,
                    LastOnlineDate = DateTime.Now
                },
                new User()
                {
                    ID = 2,
                    Name = "helmut2",
                    PasswordHash = "$2a$11$oxFGwhEGy.7meEYttK9jwusPG9Ydx7v3WsASKdllv2FDTWl30jqxm",
                    Role = "User",
                    CreationDate = DateTime.Now,
                    LastOnlineDate = DateTime.Now
                },
                new User()
                {
                    ID = 3,
                    Name = "Ingeborg-HHB",
                    PasswordHash = "$2a$11$oxFGwhEGy.7meEYttK9jwusPG9Ydx7v3WsASKdllv2FDTWl30jqxm",
                    Role = "User",
                    CreationDate = DateTime.Now,
                    LastOnlineDate = DateTime.Now
                }
            );

            modelBuilder.Entity<Retailer>().HasData
            (
                new Retailer()
                {
                    ID = 1,
                    Name = "Aldi Nord"
                },
                new Retailer()
                {
                    ID = 2,
                    Name = "Aldi Süd"
                },
                new Retailer()
                {
                    ID = 3,
                    Name = "DM"
                },
                new Retailer()
                {
                    ID = 4,
                    Name = "EDEKA"
                },
                new Retailer()
                {
                    ID = 5,
                    Name = "Globus"
                },
                new Retailer()
                {
                    ID = 6,
                    Name = "Kaufland"
                },
                new Retailer()
                {
                    ID = 7,
                    Name = "Lidl"
                },
                new Retailer()
                {
                    ID = 8,
                    Name = "Metro"
                },
                new Retailer()
                {
                    ID = 9,
                    Name = "Müller"
                },
                new Retailer()
                {
                    ID = 10,
                    Name = "Netto"
                },
                new Retailer()
                {
                    ID = 11,
                    Name = "Norma"
                },
                new Retailer()
                {
                    ID = 12,
                    Name = "Penny"
                },
                new Retailer()
                {
                    ID = 13,
                    Name = "Picnic"
                },
                new Retailer()
                {
                    ID = 14,
                    Name = "Real"
                },
                new Retailer()
                {
                    ID = 15,
                    Name = "Rewe"
                },
                new Retailer()
                {
                    ID = 16,
                    Name = "Rossmann"
                },
                new Retailer()
                {
                    ID = 17,
                    Name = "Spar"
                },
                new Retailer()
                {
                    ID = 18,
                    Name = "Trinkgut"
                },
                new Retailer()
                {
                    ID = 19,
                    Name = "Trink & Spare"
                },
                new Retailer()
                {
                    ID = 512,
                    Name = "Sonstige"
                }
            );

            modelBuilder.Entity<ProductType>().HasData
            (
                new ProductType()
                {
                    ID = 1,
                    Name = "Obst"
                },
                new ProductType()
                {
                    ID = 2,
                    Name = "Gemüse"
                },
                new ProductType()
                {
                    ID = 3,
                    Name = "Fleisch & Fisch"
                },
                new ProductType()
                {
                    ID = 4,
                    Name = "Fleisch (Bio)"
                },
                new ProductType()
                {
                    ID = 5,
                    Name = "Käse"
                },
                new ProductType()
                {
                    ID = 6,
                    Name = "Eier & Milch"
                },
                new ProductType()
                {
                    ID = 7,
                    Name = "Spezialitäten & Feinkost"
                },
                new ProductType()
                {
                    ID = 8,
                    Name = "Gebäck"
                },
                new ProductType()
                {
                    ID = 9,
                    Name = "Heißgetränke"
                },
                new ProductType()
                {
                    ID = 10,
                    Name = "Nudeln, Reis & Co"
                },
                new ProductType()
                {
                    ID = 11,
                    Name = "Saucen, Öle & Gewürze"
                },
                new ProductType()
                {
                    ID = 12,
                    Name = "Fertiggerichte & Konserven"
                },
                new ProductType()
                {
                    ID = 13,
                    Name = "Tiefkühl"
                },
                new ProductType()
                {
                    ID = 14,
                    Name = "Backen & Dessert"
                },
                new ProductType()
                {
                    ID = 15,
                    Name = "Süßes & Salziges"
                },
                new ProductType()
                {
                    ID = 16,
                    Name = "Getränke"
                },
                new ProductType()
                {
                    ID = 17,
                    Name = "Getränke (alkoholisch)"
                },
                new ProductType()
                {
                    ID = 18,
                    Name = "Drogerie"
                },
                new ProductType()
                {
                    ID = 19,
                    Name = "Baby & Kind"
                },
                new ProductType()
                {
                    ID = 20,
                    Name = "Haushalt"
                },
                new ProductType()
                {
                    ID = 21,
                    Name = "Tier"
                },
                new ProductType()
                {
                    ID = 22,
                    Name = "Kleidung"
                },
                new ProductType()
                {
                    ID = 23,
                    Name = "Elektronik"
                },
                new ProductType()
                {
                    ID = 24,
                    Name = "Wurst & Aufstriche"
                },
                new ProductType()
                {
                    ID = 512,
                    Name = "Sonstige"
                }
            );
        }

        public DbSet<ShoppingListCatalog> ShoppingListCatalogs { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
    }
}
