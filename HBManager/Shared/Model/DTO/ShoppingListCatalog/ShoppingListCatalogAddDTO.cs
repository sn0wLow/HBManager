using System.ComponentModel.DataAnnotations;

namespace HBManager.Shared
{
    public class ShoppingListCatalogAddDTO
    {
        public DateTime Date { get; set; } = DateTime.Now;
        [Range(1, int.MaxValue, ErrorMessage = "Dies ist kein valides Budget")]
        public int Budget { get; set; } = 400;
    }
}
