using System.ComponentModel.DataAnnotations;

namespace HBManager.Shared
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Benutzernamen eingeben")]
        [MinLength(4, ErrorMessage = "Ihr Benutzername muss mindestens 4 Zeichen lang sein")]
        [MaxLength(20, ErrorMessage = "Ihr Benutzername darf nicht länger als 20 Zeichen sein")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Leerzeichen und Sonderzeichen sind nicht erlaubt")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Passwort eingeben")]
        [MinLength(6, ErrorMessage = "Ihr Passwort muss mindestens 6 Zeichen lang sein")]
        [MaxLength(100, ErrorMessage = "Das Passwort darf nicht länger als 100 Zeichen sein")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "Die Passwörter stimmen nicht überein.")]
        public string RepeatPassword { get; set; } = string.Empty;
    }
}
