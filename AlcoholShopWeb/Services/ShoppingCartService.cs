using AlcoholShop.Models;
using AlcoholShopWeb.Models;
using Newtonsoft.Json;

namespace AlcoholShopWeb.Services
{
    public class ShoppingCartService
    {
        private const string SessionKey = "Cart";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public List<CartItem> GetCart()
        {
            var json = Session.GetString(SessionKey);
            return string.IsNullOrEmpty(json) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(json)!;
        }

        public void SaveCart(List<CartItem> cart)
        {
            Session.SetString(SessionKey, JsonConvert.SerializeObject(cart));
        }

        public void AddToCart(CartItem item)
        {
            var cart = GetCart();
            var existing = cart.FirstOrDefault(p => p.ProductID == item.ProductID);
            if (existing != null)
            {
                existing.Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }
            SaveCart(cart);
        }

        public void RemoveFromCart(int productID)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(p => p.ProductID == productID);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }
        }

        public void ClearCart()
        {
            SaveCart(new List<CartItem>());
        }
    }
}
