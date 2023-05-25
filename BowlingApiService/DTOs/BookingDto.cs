using BowlingData.ModelLayer;

namespace BowlingApiService.DTOs
{
    public class BookingDto
    {
        public DateTime StartDateTime { get; set; }
        public int HoursToPlay { get; set; }
        public Customer? Customer { get; set; }
        public int NoOfPlayers { get; set; }
        public int PriceId { get; set; } 
        public int LaneId { get; set; } 

        // Empty constructor
        public BookingDto() { }

        // Constructor with parameters
        public BookingDto(DateTime inStartDateTime, int inHoursToPlay, int inNoOfPlayers, Customer? customer)
        {
            StartDateTime = inStartDateTime;
            HoursToPlay = inHoursToPlay;
            NoOfPlayers = inNoOfPlayers;
            Customer = customer;
        }
    }
}
