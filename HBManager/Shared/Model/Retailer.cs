using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HBManager.Shared
{
    public class Retailer
    {
        public int ID { get; set; }

        [MaxLength(75)]
        public string Name { get; set; } = string.Empty;

        public int ColorIndex { get; set; }
        public string Color { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;

        [NotMapped, JsonInclude]
        public int TotalProductAmount = 0;

        [NotMapped, JsonInclude]
        public int TotalVisits = 0;

        [NotMapped, JsonInclude]
        public double MoneySpent = 0;

        [NotMapped, JsonInclude]
        public bool HasAnyProducts = false;

        [NotMapped, JsonInclude]
        public double TotalProductPrice = 0;

        [NotMapped, JsonInclude]
        public double AverageProductPrice = 0;

        [NotMapped, JsonInclude]
        public List<Product> Products = new();
    }
}
