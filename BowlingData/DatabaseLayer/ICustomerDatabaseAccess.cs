using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShModel;

namespace BowlingData.DatabaseLayer
{
    public interface ICustomerDatabaseAccess
    {
        Customer GetCustomerById(int id);
        List<Customer> GetPersonAll();
        int CreateCustomer(Customer aCustomer);
        bool UpdatePerson(Customer CustomerToUpdate);
        bool DeleteCustomerById(int id);
    }
}
