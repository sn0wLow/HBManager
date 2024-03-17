using System.ComponentModel.DataAnnotations;

namespace HBManager.Shared
{
    public class ConsumerAddDTO
    {
        [Required(ErrorMessage = "Verbrauchernamen eingeben")]
        [MinLength(2, ErrorMessage = "Verbrauchername muss mindestens 2 Zeichen lang sein")]
        [MaxLength(20, ErrorMessage = "Ihr Verbrauchername darf nicht länger als 20 Zeichen sein")]
        public string Name { get; set; } = string.Empty;
        [Range(0, 11, ErrorMessage = "Dies ist keine valide Farbauswahl")]
        public int ColorIndex { get; set; } = 0;
    }
}
