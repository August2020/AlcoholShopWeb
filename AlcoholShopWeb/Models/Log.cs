using System.ComponentModel.DataAnnotations;

namespace AlcoholShopWeb.Models
{
    public class Log
    {
        [Key]
        public int LogID { get; set; }
        public int? UserID { get; set; }
        public string Action { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public User? User { get; set; }
    }
}