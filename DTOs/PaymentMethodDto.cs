using System.ComponentModel.DataAnnotations;

namespace ASP_SHOP.DTOs
{
    public class PaymentMethodCreateDto
    {
        public required string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class PaymentMethodResponseDto : AuditableDtos
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }

    }
}