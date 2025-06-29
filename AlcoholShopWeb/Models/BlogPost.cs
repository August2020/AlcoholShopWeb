namespace AlcoholShopWeb.Models
{
    public class BlogPost
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? BlogCategoryID { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public BlogCategory? BlogCategory { get; set; }
        public ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();
    }
}