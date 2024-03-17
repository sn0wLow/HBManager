using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBManager.Shared
{
    public class User
    {
        public int ID { get; set; }
        [MinLength(4), MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";

        [Column(TypeName = "datetime")]
        public DateTime LastOnlineDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
