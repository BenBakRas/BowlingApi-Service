namespace BowlingApiService.DTOs
{
    public class LaneDto
    {
        public LaneDto() { }
        public LaneDto(int? laneNumber) 
        { 
            LaneNumber = laneNumber;
        }
        public int? LaneNumber { get; set; }
    }
}
