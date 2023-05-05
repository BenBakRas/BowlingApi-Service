using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShModel;

namespace BowlingData.DatabaseLayer
{
    public interface ICustomerAccess
    {
        Customer GetCustomerById(int id);
        List<Customer> GetAllCustomers();
        int CreateCustomer(Customer aCustomer);
        bool UpdateCustomer(Customer customerToUpdate);
        bool DeleteCustomerById(int id);
    }
}
