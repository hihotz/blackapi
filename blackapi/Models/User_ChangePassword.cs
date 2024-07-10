using System.ComponentModel.DataAnnotations;

namespace blackapi.Models
{
    public class User_ChangePassword
    {
        [Required]
        [StringLength(50)]
        public required string Username { get; set; }

        [Required]
        [StringLength(100)]
        public required string OldPassword { get; set; }

        [Required]
        [StringLength(100)]
        public required string NewPassword { get; set; }
    }
}
