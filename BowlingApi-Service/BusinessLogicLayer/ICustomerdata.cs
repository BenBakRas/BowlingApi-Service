using BowlingApi_Service.Dtos;

namespace BowlingApi_Service.BusinessLogicLayer
{
    public interface ICustomerdata
    {

        CustomerDto? Get(int matchingId);
        List<CustomerDto>? Get();
        int Add(CustomerDto customerToAdd);
        bool Put(CustomerDto customerToUpdate);
        bool Delete(int id);

    }
}
