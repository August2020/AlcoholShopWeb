namespace AlcoholShopWeb.Models
{
    public class BlogCategory
    {
        public int BlogCategoryID { get; set; }
        public string Name { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}