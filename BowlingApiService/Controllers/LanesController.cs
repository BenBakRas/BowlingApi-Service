using BowlingApiService.BusinessLogicLayer;
using BowlingApiService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BowlingApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanesController : ControllerBase
    {
        private readonly ILaneData _businessLogicCtrl;
        public LanesController(ILaneData inBusinessLogicCtrl) 
        {
            _businessLogicCtrl = inBusinessLogicCtrl;
        }

        [HttpGet]
        public ActionResult<List<LaneDto>> Get()
        {
            ActionResult<List<LaneDto>> foundReturn;
            // retrieve data - converted to DTO
            List<LaneDto>? foundLanes = _businessLogicCtrl.Get();
            // evaluate
            if (foundLanes != null)
            {
                if (foundLanes.Count > 0)
                {
                    foundReturn = Ok(foundLanes);                 // Statuscode 200
                }
                else
                {
                    foundReturn = new StatusCodeResult(204);    // Ok, but no content
                }
            }
            else
            {
                foundReturn = new StatusCodeResult(500);        // Internal server error
            }
            // send response back to client
            return foundReturn;
        }


        // URL: api/customers/{id}
        [HttpGet, Route("{id}")]
        public ActionResult<LaneDto> Get(int id)
        {
            ActionResult<LaneDto> foundReturn;
            // retrieve data - converted to DTO
            LaneDto? foundLane = _businessLogicCtrl.Get(id);
            // evaluate
            if (foundLane != null)
            {
                foundReturn = Ok(foundLane);       // Statuscode 200
            }
            else
            {
                foundReturn = new StatusCodeResult(404);    // Not found
            }
            // send response back to client
            return foundReturn;
        }

        // URL: api/customers
        [HttpPost]
        public ActionResult<int> PostNewLane(LaneDto inLaneDto)
        {
            ActionResult<int> foundReturn;
            int insertedId = -1;
            if (inLaneDto != null)
            {
                insertedId = _businessLogicCtrl.Add(inLaneDto);
            }
            // Evaluate
            if (insertedId > 0)
            {
                foundReturn = Ok(insertedId);
            }
            else if (insertedId == 0)
            {
                foundReturn = BadRequest();     // missing input
            }
            else
            {
                foundReturn = new StatusCodeResult(500);    // Internal server error
            }
            return foundReturn;
        }
        // URL: api/customers/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            ActionResult foundReturn;
            bool isDeleted = _businessLogicCtrl.Delete(id);
            // Evaluate
            if (isDeleted)
            {
                foundReturn = Ok(isDeleted);           // Statuscode 200
            }
            else
            {
                foundReturn = new StatusCodeResult(404);    // Not found
            }
            // send response back to client
            return foundReturn;
        }
    }
}
