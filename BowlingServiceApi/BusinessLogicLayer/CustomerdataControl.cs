using CustomerService.DTOs;
using ShModel;
using BowlingData.DatabaseLayer;
using System;

namespace CustomerService.BusinessLogicLayer
{
    public class CustomerdataControl : ICustomerData
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

        public CustomerDto? Get(int id)
        {
            CustomerDto? foundCustomerDto;
            try
            {
                Customer? foundCustomer = _customerAccess.GetCustomerById(id);
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
