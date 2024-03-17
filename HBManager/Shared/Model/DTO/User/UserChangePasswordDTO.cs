using System.ComponentModel.DataAnnotations;

namespace HBManager.Shared.Model.DTO.User
{
    public class UserChangePasswordDTO
    {
        [Required(ErrorMessage = "Passwort eingeben")]
        [MinLength(6, ErrorMessage = "Ihr Passwort muss mindestens 8 Zeichen lang sein")]
        [MaxLength(100, ErrorMessage = "Das Passwort darf nicht länger als 100 Zeichen sein")]
        public required string Password { get; set; }
        [Compare("Password", ErrorMessage = "Die Passwörter stimmen nicht überein.")]
        public string RepeatPassword { get; set; } = string.Empty;
    }
}
