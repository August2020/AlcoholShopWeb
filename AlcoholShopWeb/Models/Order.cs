namespace AlcoholShopWeb.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int? UserID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int StatusID { get; set; }
        public int DeliveryMethodID { get; set; }
        public int PaymentMethodID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }

        public User? User { get; set; }
        public OrderStatus Status { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}