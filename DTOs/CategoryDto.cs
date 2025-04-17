using System.ComponentModel.DataAnnotations;

namespace ASP_SHOP.DTOs
{
    public class CategoryCreateDto
    {
        public required string Name { get; set; }

        public string? ImageUrl { get; set; }
        public int? ParentId { get; set; }
    }

    public class CategoryResponseDto : AuditableDtos
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public int? ParentId { get; set; }
        public string? ParentName { get; set; }
    }
}