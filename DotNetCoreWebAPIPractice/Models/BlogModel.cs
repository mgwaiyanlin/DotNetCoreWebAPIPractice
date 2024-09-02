using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreWebAPIPractice.Models
{
    [Table("blogs")]
    public class BlogModel
    {
        [Key]
        public int id { get; set; }
        public string? title { get; set; }
        public string? author { get; set; }
        public string? blog_content { get; set; }
    }
}
