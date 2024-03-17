using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBManager.Shared
{
    public class ProductSearchDTO
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;

        public decimal Price { get; set; } = 1m;
        public int Quantity { get; set; } = 1;

        public int ShoppingListID { get; set; }

        public ProductType? ProductType { get; set; }
        public Retailer? Retailer { get; set; }
        public List<Consumer> Consumers { get; set; } = new List<Consumer>();

        public DateTime DateOfShopping { get; set; } = DateTime.Now;
    }
}
