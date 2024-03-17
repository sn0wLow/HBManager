using System.ComponentModel.DataAnnotations;

namespace HBManager.Shared
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Benutzernamen eingeben")]
        [MinLength(4, ErrorMessage = "Ihr Benutzername muss mindestens 4 Zeichen lang sein")]
        [MaxLength(20, ErrorMessage = "Ihr Benutzername darf nicht länger als 20 Zeichen sein")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Passwort eingeben")]
        [MinLength(6, ErrorMessage = "Ihr Passwort muss mindestens 6 Zeichen lang sein")]
        [MaxLength(100, ErrorMessage = "Das Passwort darf nicht länger als 100 Zeichen sein")]
        public string Password { get; set; } = string.Empty;
    }
}
