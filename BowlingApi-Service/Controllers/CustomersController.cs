using BowlingApi_Service.BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BowlingApi_Service.Dtos;
using System.Reflection.Metadata.Ecma335;

namespace BowlingApi_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerdata _businessLogicCtrl;

        // Constructor with Dependency Injection
        public CustomersController(ICustomerdata inBusinessLogicCtrl)
        {
            _businessLogicCtrl = inBusinessLogicCtrl;
        }

        // URL: api/customers
        [HttpGet]
        public ActionResult<List<CustomerDto>> Get()
        {
            ActionResult<List<CustomerDto>> foundReturn;
            // Retrieve data - converted to DTO
            List<CustomerDto>? foundCustomers = _businessLogicCtrl.Get();
            // Evaluate data
            if (foundCustomers != null)
            {
                if (foundCustomers.Count > 0)
                {
                    foundReturn = Ok(foundCustomers);                 // Statuscode 200
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
        public ActionResult<CustomerDto> Get(int id)
        {
            return null;
        }

        // URL: api/customers
        [HttpPost]
        public ActionResult<int> PostNewCustomer(CustomerDto inCustomerDto)
        {
            ActionResult<int> foundReturn;
            int insertedId = -1;
            if (inCustomerDto != null)
            {
                insertedId = _businessLogicCtrl.Add(inCustomerDto);
            }
            // Evaluate data
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
    }

}