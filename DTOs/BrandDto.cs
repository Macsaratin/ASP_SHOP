using System.ComponentModel.DataAnnotations;

namespace ASP_SHOP.DTOs
{
    public class BrandCreateDto
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class BrandResponseDto : AuditableDtos
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}