using BowlingApi_Service.Dtos;
using BowlingData;
using BowlingData.DatabaseLayer;
using ShModel;

namespace BowlingApi_Service.BusinessLogicLayer
{
    public class CustomerdataControl : ICustomerdata
    {
        private readonly ICustomerAccess _customerAccess;

        public CustomerdataControl(ICustomerAccess inCustomerAccess)
        {
            _customerAccess = inCustomerAccess;
        }

        public int Add(CustomerDto newCustomer)
        {
            int insertedId = 0;
            try
            {
                Customer? foundCustomer = ModelConversion.CustomerDtoConvert.ToCustomer(newCustomer);
                if (foundCustomer != null)
                {
                    insertedId = _customerAccess.CreateCustomer(foundCustomer);
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
            throw new NotImplementedException();
        }

        public CustomerDto? Get(int matchingId)
        {
            CustomerDto? foundCustomerDto;
            try
            {
                Customer? foundCustomer = _customerAccess.GetCustomerById(matchingId);
                foundCustomerDto = ModelConversion.CustomerDtoConvert.FromCustomer(foundCustomer);
            }
            catch
            {
                foundCustomerDto = null;
            }
            return foundCustomerDto;
        }

        public List<CustomerDto>? Get()
        {
            List<CustomerDto>? foundDtos;
            try
            {
                List<Customer>? foundCustomers = _customerAccess.GetAllCustomers();
                foundDtos = ModelConversion.CustomerDtoConvert.FromCustomerCollection(foundCustomers);
            }
            catch
            {
                foundDtos = null;
            }
            return foundDtos;
        }

        public bool Put(CustomerDto customerToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}