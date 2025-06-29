namespace AlcoholShopWeb.Models
{
    public class BlogTag
    {
        public int TagID { get; set; }
        public string Name { get; set; }

        public ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();
    }
}