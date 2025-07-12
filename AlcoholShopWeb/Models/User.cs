using System.ComponentModel.DataAnnotations;

namespace AlcoholShopWeb.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; } // Enum: 'Admin', 'Client'
        public DateTime CreatedAt { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Log> Logs { get; set; } = new List<Log>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

}
