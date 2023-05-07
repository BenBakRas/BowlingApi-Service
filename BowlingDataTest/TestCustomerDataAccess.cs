using Xunit.Abstractions;
using BowlingData.DatabaseLayer;
using ShModel;
using System;

namespace BowlingDataTest
{
    public class TestCustomerDataAccess
    {

        private readonly ITestOutputHelper _extraOutput;

        readonly private ICustomerAccess _CustomerAccess;
        readonly string _connectionString = "Server=localhost; Integrated Security=true; Database=BowlingDB";

        public TestCustomerDataAccess(ITestOutputHelper output)
        {
            _extraOutput = output;
            _CustomerAccess = new CustomerDatabaseAccess(_connectionString);
        }
      

        [Fact]
        public void TestGetAllCusomers()
        {
            // Arrange

            // Act
            List<Customer> readCustomers = _CustomerAccess.GetAllCustomers();
            bool customersWereRead = (readCustomers.Count > 0);
            // Print additional output
            _extraOutput.WriteLine("Number of Customers: " + readCustomers.Count);

            // Assert
            Assert.True(customersWereRead);
        }

    }
}