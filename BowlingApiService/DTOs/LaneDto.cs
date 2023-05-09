namespace BowlingApiService.DTOs
{
    public class LaneDto
    {
        public LaneDto() { }
        public LaneDto(int? InLaneNumber) 
        { 
            LaneNumber = InLaneNumber;
        }
        public int? LaneNumber { get; set; }
    }
}
