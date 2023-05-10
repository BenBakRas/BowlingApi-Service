using ShModel;

namespace BowlingApiService.DTOs
{
    public class BookingDto
    {
        public DateTime StartDateTime { get; set; }
        public int HoursToPlay { get; set; }
        public CustomerDto? CustomerDto { get; set; }
        public int? NoOfPlayers { get; set; }

        // Empty constructor
        public BookingDto() { }

        // Constructor with parameters
        public BookingDto(DateTime inStartDateTime, int inHoursToPlay, int? inNoOfPlayers, CustomerDto customerDto)
        {
            StartDateTime = inStartDateTime;
            HoursToPlay = inHoursToPlay;
            NoOfPlayers = inNoOfPlayers;
            CustomerDto = customerDto;
        }
    }
}
