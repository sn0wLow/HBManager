using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HBManager.Shared
{
    public class ShoppingList
    {
        public int ID { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; } = DateTime.Now;
        public List<Product> Products { get; set; } = new List<Product>();

        [JsonIgnore]
        public ShoppingListCatalog? ShoppingListCatalog { get; set; }
        public int ShoppingListCatalogID { get; set; }
        public Retailer? Retailer { get; set; }
        public int RetailerID { get; set; }
        public int UserID { get; set; }
    }
}
