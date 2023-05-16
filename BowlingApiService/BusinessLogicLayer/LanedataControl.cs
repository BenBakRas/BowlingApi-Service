using BowlingApiService.DTOs;
using BowlingData.DatabaseLayer;
using BowlingData.ModelLayer;
using System.Linq.Expressions;

namespace BowlingApiService.BusinessLogicLayer
{
    public class LanedataControl : ILaneData
    {
        private readonly ILaneAccess _laneAcces;

        public LanedataControl(ILaneAccess inLaneAccess)
        {
            _laneAcces = inLaneAccess;
        }

        public int Add(LaneDto newLane)
        {
            int insertedId = 0;
            try
            {
                Lane? foundLane = ModelConversion.LaneDtoConvert.ToLane(newLane);
                if(foundLane!= null)
                {
                    insertedId = _laneAcces.CreateLane(foundLane);
                }
            }
            catch
            {
                insertedId = -1;
            }
            return insertedId;
        }

        public bool Delete(int id)
        {
            try
            {
                bool isDeleted = _laneAcces.DeleteLaneById(id);
                return isDeleted;
            }
            catch
            {
                return false;
            }
        }

        public LaneDto? Get(int id)
        {
            LaneDto? foundLaneDto;
            try
            {
                Lane? foundLane = _laneAcces.GetLaneById(id);
                foundLaneDto = ModelConversion.LaneDtoConvert.FromLane(foundLane);
            }
            catch
            {
                foundLaneDto = null;
            }
            return foundLaneDto;
        }

        public List<LaneDto>? Get()
        {
            List<LaneDto>? foundDtos;
            try
            {
               
                List<Lane>? foundLanes = _laneAcces.GetAllLanes();
                foundDtos = ModelConversion.LaneDtoConvert.FromLaneCollection(foundLanes);
            }
            catch
            {
                foundDtos = null;
            }
            return foundDtos;
        }

        public bool Put(LaneDto laneToUpdate, int idToUpdate)
        {
            try
            {
                Lane? updatedLane = ModelConversion.LaneDtoConvert.ToLane(laneToUpdate, idToUpdate);
                return _laneAcces.UpdateLane(updatedLane);


            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
