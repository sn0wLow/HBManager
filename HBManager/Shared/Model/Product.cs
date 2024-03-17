using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HBManager.Shared
{
    public class Product
    {
        public int ID { get; set; }
        [MaxLength(25), MinLength(2)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Note { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 1m;
        public int Quantity { get; set; } = 1;
        public int UserID { get; set; }
        [JsonIgnore]
        public ShoppingList? ShoppingList { get; set; }
        public int ShoppingListID { get; set; }

        public ProductType? ProductType { get; set; }
        public int ProductTypeID { get; set; }
        public bool IsFavorited { get; set; } = false;

        public List<Consumer> Consumers { get; set; } = new List<Consumer>();
    }
}
