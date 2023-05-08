namespace BowlingApiService.DTOs
{
    public class LanedataCreateDto
    {
        public LanedataCreateDto() { }
        public LanedataCreateDto(int laneNumber)
        {
            LaneNumber = laneNumber;
        }
        public int LaneNumber { get; set; }
    }
}

