using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_SHOP.Models
{
    [Table("category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? ImageUrl { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

    }
}