using System.ComponentModel.DataAnnotations;

namespace AlcoholShopWeb.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int VolumeML { get; set; }
        public decimal AlcoholPercentage { get; set; }
        public int? Year { get; set; }
        public string? AgingDuration { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool Availability { get; set; }
        public string? ImageURL { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? CategoryID { get; set; }
        public int? ProducerID { get; set; }
        public int? CountryID { get; set; }
        public int? ProductionMethodID { get; set; }
        public int? AgingID { get; set; }

        public Category? Category { get; set; }
        public Producer? Producer { get; set; }
        public Country? Country { get; set; }
        public ProductionMethod? ProductionMethod { get; set; }
        public Aging? Aging { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

}
