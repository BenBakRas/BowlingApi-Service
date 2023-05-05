using CustomerService.DTOs;
namespace CustomerService.BusinessLogicLayer
{
    public interface ICustomerData
    {

        CustomerDto? Get(int id);
        List<CustomerDto>? Get();
        int Add(CustomerDto customerToAdd);
        bool Put(CustomerDto customerToUpdate);
        bool Delete(int id);
    }
}
