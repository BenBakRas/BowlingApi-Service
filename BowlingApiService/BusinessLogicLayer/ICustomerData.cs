using BowlingApiService.DTOs;
using ShModel;

namespace BowlingApiService.BusinessLogicLayer
{
    public interface ICustomerData {

        CustomerDto? Get(int id);
        List<CustomerDto>? Get();
        int Add(CustomerDto customerToAdd);
        bool Put(CustomerDto customerToUpdate, int idToUpdate);
        bool Delete(int id);
    }
}
