namespace AlcoholShopWeb.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public int? Rating { get; set; } // Between 1 and 5
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }

}
