using System.ComponentModel.DataAnnotations;

namespace blackapi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Username { get; set; }

        [StringLength(100)]
        [Required]
        public required string Password { get; set; }
    }
}
