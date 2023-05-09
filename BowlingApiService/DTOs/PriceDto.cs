namespace BowlingApiService.DTOs
{
    public class PriceDto
    {

        // Empty constructor
        public PriceDto() { }

        // Constructor with parameters
        public PriceDto(double? normalPrice, double? specialPrice, string? weekday)
        {
            NormalPrice = normalPrice;
            SpecialPrice = specialPrice;
            Weekday = weekday;
        }
        public double? NormalPrice { get; set; }
        public double? SpecialPrice { get; set; }
        public string? Weekday { get; set; }
    }
}
