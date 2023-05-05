namespace CustomerService.DTOs
{
    public class CustomerDto
    {

        public CustomerDto() { }

        public CustomerDto(string? inFirstName, string? inLastName, string? inEmail, string? inPhone) 
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
        public string? FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
