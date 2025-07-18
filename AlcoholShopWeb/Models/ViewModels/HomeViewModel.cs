using System.Collections.Generic;

namespace AlcoholShopWeb.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Product> FeaturedProducts { get; set; }
        public List<BlogPost> LatestBlogPosts { get; set; }
    }
}
