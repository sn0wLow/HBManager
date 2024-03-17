using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HBManager.Shared
{
    public class Consumer
    {
        public int ID { get; set; }
        [MaxLength(20), MinLength(2)]
        public string Name { get; set; } = string.Empty;
        public int ColorIndex { get; set; }
        public string Color { get; set; } = string.Empty;
        public int UserID { get; set; }

        [NotMapped, JsonInclude]
        public int GlobalTotalUniqueProductAmount = 0;

        [NotMapped, JsonInclude]
        public int TotalProductAmount = 0;

        [NotMapped, JsonInclude]
        public bool HasAnyProducts = false;

        [NotMapped, JsonInclude]
        public double TotalProductPrice = 0;

        [NotMapped, JsonInclude]
        public double AverageProductPrice = 0;


        [JsonIgnore]
        public List<Product> Products { get; set; } = new List<Product>();

    }
}
