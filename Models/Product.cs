using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_SHOP.Models
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được để trống.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = "/images/default.png";
    }
}
