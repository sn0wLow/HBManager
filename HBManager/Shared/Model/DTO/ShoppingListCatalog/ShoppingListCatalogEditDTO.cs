using System.ComponentModel.DataAnnotations;

namespace HBManager.Shared
{
    public class ShoppingListCatalogEditDTO
    {
        public int ID { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Dies ist kein valides Budget")]
        public int Budget { get; set; } = 400;
    }
}
