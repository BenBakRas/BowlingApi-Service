using Microsoft.Extensions.Configuration;
using ShModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingData.DatabaseLayer
{
    public class CustomerDatabaseAccess : ICustomerAccess
    {

        readonly string? _connectionString;
        public CustomerDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public CustomerDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }
        public int CreateCustomer(Customer aCustomer)
        {
            int insertedId = -1;
            //
            string insertString = "insert into Customer(firstName, lastName, email, phone) OUTPUT INSERTED.ID values(@FirstName, @LastName, @Email, @Phone)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand CreateCommand = new SqlCommand(insertString, con))
            {
                // Prepace SQL
                SqlParameter fNameParam = new("@FirstName", aCustomer.FirstName);
                CreateCommand.Parameters.Add(fNameParam);
                SqlParameter lNameParam = new("@LastName", aCustomer.LastName);
                CreateCommand.Parameters.Add(lNameParam);
                SqlParameter EmailParam = new("@Email", aCustomer.Email);
                CreateCommand.Parameters.Add(EmailParam);
                SqlParameter phoneParam = new("Phone", aCustomer.Phone);
                CreateCommand.Parameters.Add(phoneParam);
                //
                con.Open();
                // Execute save and read generated key (ID)
                insertedId = (int)CreateCommand.ExecuteScalar();
            }
            return insertedId;
        }

        public bool DeleteCustomerById(int id)
        {
            bool isDeleted = false;
            string deleteString = "DELETE FROM Customer WHERE id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
            {
                deleteCommand.Parameters.AddWithValue("@Id", id);

                con.Open();
                int rowsAffected = deleteCommand.ExecuteNonQuery();

                isDeleted = (rowsAffected > 0);
                }

            return isDeleted;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> foundCustomers;
            Customer readCustomer;
            //
            string queryString = "select id, firstName, lastName, email, phone from Customer";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                // Execute read
                SqlDataReader customerReader = readCommand.ExecuteReader();
                // Collect data
                foundCustomers = new List<Customer>();
                while (customerReader.Read())
                {
                    readCustomer = GetCustomerFromReader(customerReader);
                    foundCustomers.Add(readCustomer);
                }
            }
            return foundCustomers;
        }

        public Customer GetCustomerById(int id)
        {
            Customer foundCustomer;
            //
            string queryString = "select id, firstName, lastName, email, phone from Customer where id = @Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                // Prepace SQL
                SqlParameter idParam = new SqlParameter("@Id", id);
                readCommand.Parameters.Add(idParam);
                //
                con.Open();
                // Execute read
                SqlDataReader customerReader = readCommand.ExecuteReader();
                foundCustomer = new Customer();
                while (customerReader.Read())
                {
                    foundCustomer = GetCustomerFromReader(customerReader);
                }
            }
            return foundCustomer;
        }

        public bool UpdateCustomer(Customer customerToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE Customer SET firstName = @FirstName, lastName = @LastName, email = @Email, phone = @Phone WHERE id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", customerToUpdate.Id); // Add the missing @Id parameter
                updateCommand.Parameters.AddWithValue("@FirstName", customerToUpdate.FirstName);
                updateCommand.Parameters.AddWithValue("@LastName", customerToUpdate.LastName);
                updateCommand.Parameters.AddWithValue("@Email", customerToUpdate.Email);
                updateCommand.Parameters.AddWithValue("@Phone", customerToUpdate.Phone);

                con.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();

                if(isUpdated = (rowsAffected > 0))
                {
                    return isUpdated;
                }
                else
                {
                    return false; 
                }
            }
            
        }


        private Customer GetCustomerFromReader(SqlDataReader customerReader)
        {
            Customer foundCustomer;
            int tempId;
            string tempFirstName, tempLastName, tempEmail, tempPhone;
            // Fetch values
            tempId = customerReader.GetInt32(customerReader.GetOrdinal("id"));
            tempFirstName = customerReader.GetString(customerReader.GetOrdinal("firstName"));
            tempLastName = customerReader.GetString(customerReader.GetOrdinal("lastName"));
            tempEmail = customerReader.GetString(customerReader.GetOrdinal("email"));
            tempPhone = customerReader.GetString(customerReader.GetOrdinal("phone"));
            // Create object
            foundCustomer = new Customer(tempId, tempFirstName, tempLastName, tempEmail, tempPhone);
            return foundCustomer;
        }


    }

}

