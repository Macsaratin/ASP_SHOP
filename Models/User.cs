using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_SHOP.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Avatar { get; set; }

        public string? Email { get; set; }
    }
}