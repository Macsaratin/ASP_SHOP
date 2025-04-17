using System.ComponentModel.DataAnnotations;

namespace ASP_SHOP.DTOs
{
    public class ImageCreateDto
    {
        public required string ImageUrl { get; set; }

        public required int ProductId { get; set; }
    }

    public class ImageResponseDto
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
    }
}