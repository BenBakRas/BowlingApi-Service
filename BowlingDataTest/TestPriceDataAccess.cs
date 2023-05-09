using BowlingData.DatabaseLayer;
using ShModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BowlingDataTest
{
    public class TestPriceDataAccess
    {
        private readonly ITestOutputHelper _extraOutput;

        readonly private IPriceAccess _pAccess;

        readonly string _connectionString = "Server=localhost; Integrated Security=true; Database=BowlingTest";

        public TestPriceDataAccess(ITestOutputHelper output)
        {
            _extraOutput = output;
            _pAccess = new PriceDatabaseAccess(_connectionString);
        }

        [Fact]
        public void TestGetAllPrices()
        {
            // Arrange

            // Act
            List<Price> readPrices = _pAccess.GetAllPrices();
            bool pricesWereRead = (readPrices.Count > 0);
            // Print additional output
            _extraOutput.WriteLine("Number of prices: " + readPrices.Count);

            // Assert
            Assert.True(pricesWereRead);
        }
    }
}
