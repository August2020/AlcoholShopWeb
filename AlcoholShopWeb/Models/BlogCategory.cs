using System.ComponentModel.DataAnnotations;

namespace AlcoholShopWeb.Models
{
    public class BlogCategory
    {
        [Key]
        public int BlogCategoryID { get; set; }
        public string Name { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}