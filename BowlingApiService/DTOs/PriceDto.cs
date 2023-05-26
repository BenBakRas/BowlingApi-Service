namespace BowlingApiService.DTOs
{
    public class PriceDto
    {

        // Empty constructor
        public PriceDto() { }

        // Constructor with parameters
        public PriceDto(double? normalPrice, string? weekday)
        {
            NormalPrice = normalPrice;
            Weekday = weekday;
        }
        public double? NormalPrice { get; set; }
        public string? Weekday { get; set; }
    }
}
