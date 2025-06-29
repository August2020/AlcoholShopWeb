namespace AlcoholShopWeb.Models
{
    public class BlogPostTag
    {
        public int PostID { get; set; }
        public int TagID { get; set; }

        public BlogPost Post { get; set; }
        public BlogTag Tag { get; set; }
    }
}
