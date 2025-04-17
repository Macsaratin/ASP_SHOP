namespace ASP_SHOP.DTOs
{
    public class AddressResponseDto : AuditableDtos
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Street { get; set; }

        public string Ward { get; set; }

        public string District { get; set; }

        public string City { get; set; }

        public int UserId { get; set; }

        public string UserFullName { get; set; } 
    }
}
