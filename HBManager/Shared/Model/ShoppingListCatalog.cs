using System.ComponentModel.DataAnnotations.Schema;

namespace HBManager.Shared
{
    public class ShoppingListCatalog
    {
        public int ID { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; } = DateTime.Now;
        public int Budget { get; set; } = 400;

        public List<ShoppingList> ShoppingLists { get; set; } = new List<ShoppingList>();
        public int UserID { get; set; }
    }
}
