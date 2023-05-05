namespace CustomerService.DTOs
{
    public class CustomerdataCreateDto
    {
        public CustomerdataCreateDto() { }

        public CustomerdataCreateDto(string? inFirstName, string? inLastName, string? inEmail, string? inPhone)
        {
            FirstName = inFirstName;
            LastName = inLastName;
            Email = inEmail;
            Phone = inPhone;
        }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

    }
}
