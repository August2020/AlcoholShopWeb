using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlcoholShopWeb.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [ForeignKey(nameof(ProductID))]
        public Product Product { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
