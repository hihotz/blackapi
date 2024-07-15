using System.ComponentModel.DataAnnotations;

namespace blackapi.Models
{
    public class Production
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string ProductName { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime ProductionDate { get; set; } = DateTime.Now;
    }
}
