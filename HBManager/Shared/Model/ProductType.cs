using System.ComponentModel.DataAnnotations;

namespace HBManager.Shared
{
    public class ProductType
    {
        public int ID { get; set; }

        [MaxLength(75)]
        public string Name { get; set; } = string.Empty;
        public int ColorIndex { get; set; }
        public string Color { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
    }
}
