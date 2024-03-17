using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBManager.Shared
{
    public class ProductEditDTO
    {
        [Required(ErrorMessage = "Produkttitel eingeben")]
        [MinLength(2, ErrorMessage = "Produkttitel muss mindestens 2 Zeichen lang sein")]
        [MaxLength(25, ErrorMessage = "Ihr Produkttitel darf nicht länger als 25 Zeichen sein")]
        [RegularExpression(@"^[^\s].{0,23}[^\s]$", ErrorMessage = "Leerzeichen am Anfang oder Ende sind nicht erlaubt")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Preis eingeben")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 5000, ErrorMessage = "Dies ist kein valider Preis")]
        public decimal Price { get; set; }
        [Range(1, 20, ErrorMessage = "Dies ist keine valide Anzahl")]
        public int Quantity { get; set; }

        public int ID { get; set; }
        public string? Note { get; set; }
        public int ProductTypeID { get; set; }
        public List<int> ConsumerIDs { get; set; } = new List<int>();
    }
}
